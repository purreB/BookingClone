using BookingClone.Application.DTOs;
using BookingClone.Domain;
using BookingClone.Infrastructure.Auth;
using BookingClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(
    UserManager<ApplicationUser> userManager,
    IUserRepository userRepository,
    JwtTokenService jwtTokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(
        [FromBody] RegisterRequestDto request,
        CancellationToken cancellationToken)
    {
        var normalizedRole = NormalizeRole(request.Role);
        if (!IsSupportedRole(normalizedRole))
        {
            return BadRequest("Role must be either 'Guest' or 'Staff'.");
        }

        var normalizedEmail = NormalizeEmail(request.Email);
        var existingUser = await userManager.FindByEmailAsync(normalizedEmail);
        if (existingUser is not null)
        {
            return Conflict("A user with this email already exists.");
        }

        var roleAlreadyExists = normalizedRole == "Guest"
            ? await userRepository.GetGuestByEmailAsync(normalizedEmail) is not null
            : await userRepository.GetStaffByEmailAsync(normalizedEmail) is not null;

        if (roleAlreadyExists)
        {
            return Conflict("A user profile with this email already exists.");
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = normalizedEmail,
            Email = normalizedEmail,
            FullName = request.FullName.Trim(),
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return BadRequest(createResult.Errors.Select(error => error.Description));
        }

        var roleResult = await userManager.AddToRoleAsync(user, normalizedRole);
        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(user);
            return BadRequest(roleResult.Errors.Select(error => error.Description));
        }

        try
        {
            if (normalizedRole == "Guest")
            {
                await userRepository.AddGuestAsync(new Guest
                {
                    Id = user.Id,
                    Name = user.FullName,
                    Email = normalizedEmail
                });
            }
            else
            {
                await userRepository.AddStaffAsync(new StaffUser
                {
                    Id = user.Id,
                    Name = user.FullName,
                    Email = normalizedEmail,
                    IsOwner = false
                });
            }
        }
        catch
        {
            await userManager.DeleteAsync(user);
            throw;
        }

        var (token, expiresAtUtc, roles) = await jwtTokenService.CreateTokenAsync(user, cancellationToken);

        return Ok(new AuthResponseDto
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAtUtc,
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = NormalizeEmail(request.Email);
        var user = await userManager.FindByEmailAsync(normalizedEmail);
        if (user is null)
        {
            return Unauthorized("Invalid credentials.");
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return Unauthorized("Invalid credentials.");
        }

        var hasActiveProfile = await HasActiveUserProfileAsync(user);
        if (!hasActiveProfile)
        {
            return Unauthorized("Invalid credentials.");
        }

        var (token, expiresAtUtc, roles) = await jwtTokenService.CreateTokenAsync(user, cancellationToken);

        return Ok(new AuthResponseDto
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAtUtc,
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles
        });
    }

    private static bool IsSupportedRole(string role) =>
        role is "Guest" or "Staff";

    private static string NormalizeEmail(string email) =>
        email.Trim().ToLowerInvariant();

    private static string NormalizeRole(string role)
    {
        var normalized = role.Trim().ToLowerInvariant();
        return normalized switch
        {
            "guest" => "Guest",
            "staff" => "Staff",
            _ => role.Trim()
        };
    }

    private async Task<bool> HasActiveUserProfileAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var hasGuestRole = roles.Contains("Guest", StringComparer.OrdinalIgnoreCase);
        var hasStaffRole = roles.Contains("Staff", StringComparer.OrdinalIgnoreCase);

        if (hasGuestRole)
        {
            var guest = await userRepository.GetGuestByIdAsync(user.Id);
            if (guest is null)
            {
                return false;
            }
        }

        if (hasStaffRole)
        {
            var staff = await userRepository.GetStaffByIdAsync(user.Id);
            if (staff is null)
            {
                return false;
            }
        }

        return hasGuestRole || hasStaffRole;
    }
}
