namespace BookingClone.Application.Abstractions.Identity;

public interface IIdentityAccountService
{
    Task<IdentityAccount?> FindByEmailAsync(string normalizedEmail);
    Task<IdentityAccount?> FindByIdAsync(Guid userId);
    Task<bool> CheckPasswordAsync(Guid userId, string password);
    Task<IReadOnlyList<string>> GetRolesAsync(Guid userId);
    Task<IdentityActionResult> CreateAsync(string normalizedEmail, string fullName, string password);
    Task<IdentityActionResult> AddToRoleAsync(Guid userId, string role);
    Task<IdentityActionResult> DeleteAsync(Guid userId);
}
