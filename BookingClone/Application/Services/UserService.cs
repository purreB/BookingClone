using AutoMapper;
using BookingClone.Application.DTOs;
using BookingClone.Domain;
using BookingClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace BookingClone.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<GuestDto?> GetGuestByIdAsync(Guid id)
    {
        var guest = await userRepository.GetGuestByIdAsync(id);
        return guest == null ? null : mapper.Map<GuestDto>(guest);
    }

    public async Task<StaffUserDto?> GetStaffByIdAsync(Guid id)
    {
        var staff = await userRepository.GetStaffByIdAsync(id);
        return staff == null ? null : mapper.Map<StaffUserDto>(staff);
    }

    public async Task<IdentityOperationResult> DeleteGuestWithIdentityAsync(Guid id)
    {
        await userRepository.DeleteGuestAsync(id);
        return await DeleteIdentityUserIfExistsAsync(id);
    }

    public async Task<IdentityOperationResult> DeleteStaffWithIdentityAsync(Guid id)
    {
        await userRepository.DeleteStaffAsync(id);
        return await DeleteIdentityUserIfExistsAsync(id);
    }

    private async Task<IdentityOperationResult> DeleteIdentityUserIfExistsAsync(Guid userId)
    {
        var identityUser = await userManager.FindByIdAsync(userId.ToString());
        if (identityUser is null)
        {
            return IdentityOperationResult.Success();
        }

        var deleteResult = await userManager.DeleteAsync(identityUser);
        if (deleteResult.Succeeded)
        {
            return IdentityOperationResult.Success();
        }

        var errors = deleteResult.Errors.Select(error => error.Description);
        return IdentityOperationResult.Failure(string.Join("; ", errors));
    }
}
