namespace BookingClone.Domain;

public interface IUserRepository
{
    Task<Guest?> GetGuestByIdAsync(Guid id);
    Task<StaffUser?> GetStaffByIdAsync(Guid id);
    Task AddGuestAsync(Guest guest);
    Task AddStaffAsync(StaffUser staff);
    Task UpdateGuestAsync(Guest guest);
    Task UpdateStaffAsync(StaffUser staff);
    Task DeleteGuestAsync(Guid id);
    Task DeleteStaffAsync(Guid id);
}
