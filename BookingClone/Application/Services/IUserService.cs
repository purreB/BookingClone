using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IUserService
{
    Task<GuestDto?> GetGuestByIdAsync(Guid id);
    Task<StaffUserDto?> GetStaffByIdAsync(Guid id);
    Task<GuestDto> AddGuestAsync(GuestDto guest);
    Task<StaffUserDto> AddStaffAsync(StaffUserDto staff);
    Task UpdateGuestAsync(GuestDto guest);
    Task UpdateStaffAsync(StaffUserDto staff);
    Task DeleteGuestAsync(Guid id);
    Task DeleteStaffAsync(Guid id);
}
