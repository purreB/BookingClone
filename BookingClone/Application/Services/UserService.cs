using BookingClone.Application.DTOs;
using BookingClone.Domain;

namespace BookingClone.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public GuestDto? GetGuestById(Guid id)
    {
        var guest = _userRepository.GetGuestById(id);
        if (guest == null) return null;
        return new GuestDto { Id = guest.Id, Name = guest.Name, Email = guest.Email };
    }

    public StaffUserDto? GetStaffById(Guid id)
    {
        var staff = _userRepository.GetStaffById(id);
        if (staff == null) return null;
        return new StaffUserDto { Id = staff.Id, Name = staff.Name, Email = staff.Email, IsOwner = staff.IsOwner };
    }

    public void AddGuest(GuestDto guest)
    {
        var entity = new Guest { Id = guest.Id, Name = guest.Name, Email = guest.Email };
        _userRepository.AddGuest(entity);
    }

    public void AddStaff(StaffUserDto staff)
    {
        var entity = new StaffUser { Id = staff.Id, Name = staff.Name, Email = staff.Email, IsOwner = staff.IsOwner };
        _userRepository.AddStaff(entity);
    }

    public void UpdateGuest(GuestDto guest)
    {
        var entity = new Guest { Id = guest.Id, Name = guest.Name, Email = guest.Email };
        _userRepository.UpdateGuest(entity);
    }

    public void UpdateStaff(StaffUserDto staff)
    {
        var entity = new StaffUser { Id = staff.Id, Name = staff.Name, Email = staff.Email, IsOwner = staff.IsOwner };
        _userRepository.UpdateStaff(entity);
    }

    public void DeleteGuest(Guid id) => _userRepository.DeleteGuest(id);
    public void DeleteStaff(Guid id) => _userRepository.DeleteStaff(id);
}
