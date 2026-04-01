namespace BookingClone.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<Guest?> GetGuestByIdAsync(Guid id);
    Task<StaffUser?> GetStaffByIdAsync(Guid id);
    Task<Guest?> GetGuestByEmailAsync(string email);
    Task<StaffUser?> GetStaffByEmailAsync(string email);
    Task AddGuestAsync(Guest guest);
    Task AddStaffAsync(StaffUser staff);
    Task DeleteGuestAsync(Guid id);
    Task DeleteStaffAsync(Guid id);
}
