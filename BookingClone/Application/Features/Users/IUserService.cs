using BookingClone.Application.Abstractions.Identity;
using BookingClone.Application.DTOs;

namespace BookingClone.Application.Features.Users;

public interface IUserService
{
    Task<GuestDto?> GetGuestByIdAsync(Guid id);
    Task<StaffUserDto?> GetStaffByIdAsync(Guid id);
    Task<IdentityActionResult> DeleteGuestWithIdentityAsync(Guid id);
    Task<IdentityActionResult> DeleteStaffWithIdentityAsync(Guid id);
}
