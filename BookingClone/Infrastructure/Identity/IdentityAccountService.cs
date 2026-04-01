using BookingClone.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace BookingClone.Infrastructure.Identity;

public sealed class IdentityAccountService(UserManager<ApplicationUser> userManager) : IIdentityAccountService
{
    public async Task<IdentityAccount?> FindByEmailAsync(string normalizedEmail)
    {
        var user = await userManager.FindByEmailAsync(normalizedEmail);
        return user is null ? null : ToIdentityAccount(user);
    }

    public async Task<IdentityAccount?> FindByIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user is null ? null : ToIdentityAccount(user);
    }

    public async Task<bool> CheckPasswordAsync(Guid userId, string password)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user is not null && await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IReadOnlyList<string>> GetRolesAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return [];
        }

        var roles = await userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<IdentityActionResult> CreateAsync(string normalizedEmail, string fullName, string password)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = normalizedEmail,
            Email = normalizedEmail,
            FullName = fullName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        return ToActionResult(result);
    }

    public async Task<IdentityActionResult> AddToRoleAsync(Guid userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return IdentityActionResult.Failure("Identity user does not exist.");
        }

        var result = await userManager.AddToRoleAsync(user, role);
        return ToActionResult(result);
    }

    public async Task<IdentityActionResult> DeleteAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return IdentityActionResult.Success();
        }

        var result = await userManager.DeleteAsync(user);
        return ToActionResult(result);
    }

    private static IdentityAccount ToIdentityAccount(ApplicationUser user) =>
        new(user.Id, user.Email ?? string.Empty, user.FullName);

    private static IdentityActionResult ToActionResult(IdentityResult result)
    {
        if (result.Succeeded)
        {
            return IdentityActionResult.Success();
        }

        var errors = string.Join("; ", result.Errors.Select(error => error.Description));
        return IdentityActionResult.Failure(errors);
    }
}
