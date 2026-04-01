using BookingClone.Application.Abstractions.Auth;
using BookingClone.Application.Abstractions.Identity;
using BookingClone.Application.Common.Results;
using BookingClone.Application.DTOs;
using BookingClone.Domain.Users;
using BookingClone.Domain.Users.Repositories;

namespace BookingClone.Application.Features.Auth;

public class AuthService(
    IIdentityAccountService identityAccountService,
    IUserRepository userRepository,
    ITokenService tokenService) : IAuthService
{
    private const string InvalidCredentialsMessage = "Invalid credentials.";
    private const string InvalidRoleMessage = "Role must be either 'Guest' or 'Staff'.";

    public async Task<AuthOperationResult<AuthResponseDto>> RegisterAsync(
        RegisterRequestDto request,
        CancellationToken cancellationToken)
    {
        var normalizedRole = NormalizeRole(request.Role);
        if (!IsSupportedRole(normalizedRole))
        {
            return AuthOperationResult<AuthResponseDto>.Failure(AuthErrorType.Validation, InvalidRoleMessage);
        }

        var normalizedEmail = NormalizeEmail(request.Email);
        var existingUser = await identityAccountService.FindByEmailAsync(normalizedEmail);
        if (existingUser is not null)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(
                AuthErrorType.Conflict,
                "A user with this email already exists.");
        }

        var roleAlreadyExists = normalizedRole == "Guest"
            ? await userRepository.GetGuestByEmailAsync(normalizedEmail) is not null
            : await userRepository.GetStaffByEmailAsync(normalizedEmail) is not null;

        if (roleAlreadyExists)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(
                AuthErrorType.Conflict,
                "A user profile with this email already exists.");
        }

        var createResult = await identityAccountService.CreateAsync(
            normalizedEmail,
            request.FullName.Trim(),
            request.Password);
        if (!createResult.IsSuccess)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(
                AuthErrorType.Validation,
                createResult.ErrorMessage ?? "Failed to create identity user.");
        }

        var createdUser = await identityAccountService.FindByEmailAsync(normalizedEmail);
        if (createdUser is null)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(
                AuthErrorType.Validation,
                "Failed to load identity user after creation.");
        }

        var roleResult = await identityAccountService.AddToRoleAsync(createdUser.Id, normalizedRole);
        if (!roleResult.IsSuccess)
        {
            await identityAccountService.DeleteAsync(createdUser.Id);
            return AuthOperationResult<AuthResponseDto>.Failure(
                AuthErrorType.Validation,
                roleResult.ErrorMessage ?? "Failed to assign role.");
        }

        try
        {
            if (normalizedRole == "Guest")
            {
                await userRepository.AddGuestAsync(new Guest
                {
                    Id = createdUser.Id,
                    Name = createdUser.FullName,
                    Email = normalizedEmail
                });
            }
            else
            {
                await userRepository.AddStaffAsync(new StaffUser
                {
                    Id = createdUser.Id,
                    Name = createdUser.FullName,
                    Email = normalizedEmail,
                    IsOwner = false
                });
            }
        }
        catch
        {
            await identityAccountService.DeleteAsync(createdUser.Id);
            throw;
        }

        var response = await BuildAuthResponseAsync(createdUser, cancellationToken);
        return AuthOperationResult<AuthResponseDto>.Success(response);
    }

    public async Task<AuthOperationResult<AuthResponseDto>> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = NormalizeEmail(request.Email);
        var user = await identityAccountService.FindByEmailAsync(normalizedEmail);
        if (user is null)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(AuthErrorType.Unauthorized, InvalidCredentialsMessage);
        }

        var passwordValid = await identityAccountService.CheckPasswordAsync(user.Id, request.Password);
        if (!passwordValid)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(AuthErrorType.Unauthorized, InvalidCredentialsMessage);
        }

        var hasActiveProfile = await HasActiveUserProfileAsync(user);
        if (!hasActiveProfile)
        {
            return AuthOperationResult<AuthResponseDto>.Failure(AuthErrorType.Unauthorized, InvalidCredentialsMessage);
        }

        var response = await BuildAuthResponseAsync(user, cancellationToken);
        return AuthOperationResult<AuthResponseDto>.Success(response);
    }

    private async Task<AuthResponseDto> BuildAuthResponseAsync(
        IdentityAccount user,
        CancellationToken cancellationToken)
    {
        var roles = await identityAccountService.GetRolesAsync(user.Id);
        var tokenResult = await tokenService.CreateTokenAsync(
            new TokenPayload(user.Id, user.Email, user.FullName, roles),
            cancellationToken);
        return new AuthResponseDto
        {
            AccessToken = tokenResult.AccessToken,
            ExpiresAtUtc = tokenResult.ExpiresAtUtc,
            Email = user.Email,
            FullName = user.FullName,
            Roles = roles
        };
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

    private async Task<bool> HasActiveUserProfileAsync(IdentityAccount user)
    {
        var roles = await identityAccountService.GetRolesAsync(user.Id);
        var hasGuestRole = roles.Contains("Guest", StringComparer.OrdinalIgnoreCase);
        var hasStaffRole = roles.Contains("Staff", StringComparer.OrdinalIgnoreCase);

        if (hasGuestRole && await userRepository.GetGuestByIdAsync(user.Id) is null)
        {
            return false;
        }

        if (hasStaffRole && await userRepository.GetStaffByIdAsync(user.Id) is null)
        {
            return false;
        }

        return hasGuestRole || hasStaffRole;
    }
}
