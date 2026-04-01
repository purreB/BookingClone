using AutoMapper;
using BookingClone.Application.Abstractions.Identity;
using BookingClone.Application.DTOs;
using BookingClone.Domain.Users.Repositories;

namespace BookingClone.Application.Features.Users;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IIdentityAccountService identityAccountService) : IUserService
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

    public async Task<IdentityActionResult> DeleteGuestWithIdentityAsync(Guid id)
    {
        await userRepository.DeleteGuestAsync(id);
        return await identityAccountService.DeleteAsync(id);
    }

    public async Task<IdentityActionResult> DeleteStaffWithIdentityAsync(Guid id)
    {
        await userRepository.DeleteStaffAsync(id);
        return await identityAccountService.DeleteAsync(id);
    }
}
