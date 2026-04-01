using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IUserService
{
    Task<GuestDto?> GetGuestByIdAsync(Guid id);
    Task<StaffUserDto?> GetStaffByIdAsync(Guid id);
    Task<IdentityOperationResult> DeleteGuestWithIdentityAsync(Guid id);
    Task<IdentityOperationResult> DeleteStaffWithIdentityAsync(Guid id);
}
