using BookingClone.Domain;

namespace BookingClone.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<Guest> _guests = new();
    private readonly List<StaffUser> _staff = new();

    public Guest? GetGuestById(Guid id) => _guests.FirstOrDefault(g => g.Id == id);
    public StaffUser? GetStaffById(Guid id) => _staff.FirstOrDefault(s => s.Id == id);
    public void AddGuest(Guest guest) => _guests.Add(guest);
    public void AddStaff(StaffUser staff) => _staff.Add(staff);
    public void UpdateGuest(Guest guest)
    {
        var idx = _guests.FindIndex(g => g.Id == guest.Id);
        if (idx >= 0) _guests[idx] = guest;
    }
    public void UpdateStaff(StaffUser staff)
    {
        var idx = _staff.FindIndex(s => s.Id == staff.Id);
        if (idx >= 0) _staff[idx] = staff;
    }
    public void DeleteGuest(Guid id) => _guests.RemoveAll(g => g.Id == id);
    public void DeleteStaff(Guid id) => _staff.RemoveAll(s => s.Id == id);
}
