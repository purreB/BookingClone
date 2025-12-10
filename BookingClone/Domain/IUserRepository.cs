namespace BookingClone.Domain;

public interface IUserRepository
{
    Guest? GetGuestById(Guid id);
    StaffUser? GetStaffById(Guid id);
    void AddGuest(Guest guest);
    void AddStaff(StaffUser staff);
    void UpdateGuest(Guest guest);
    void UpdateStaff(StaffUser staff);
    void DeleteGuest(Guid id);
    void DeleteStaff(Guid id);
}
