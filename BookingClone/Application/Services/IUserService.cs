using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IUserService
{
    GuestDto? GetGuestById(Guid id);
    StaffUserDto? GetStaffById(Guid id);
    void AddGuest(GuestDto guest);
    void AddStaff(StaffUserDto staff);
    void UpdateGuest(GuestDto guest);
    void UpdateStaff(StaffUserDto staff);
    void DeleteGuest(Guid id);
    void DeleteStaff(Guid id);
}
