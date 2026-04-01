using Microsoft.EntityFrameworkCore;
using BookingClone.Domain;
using BookingClone.Infrastructure.Data;

namespace BookingClone.Infrastructure.Repositories;

public class UserRepository(BookingCloneDbContext context) : IUserRepository
{
    public async Task<Guest?> GetGuestByIdAsync(Guid id) =>
        await context.Guests.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<StaffUser?> GetStaffByIdAsync(Guid id) =>
        await context.StaffUsers.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Guest?> GetGuestByEmailAsync(string email) =>
        await context.Guests.FirstOrDefaultAsync(g => g.Email == email);

    public async Task<StaffUser?> GetStaffByEmailAsync(string email) =>
        await context.StaffUsers.FirstOrDefaultAsync(s => s.Email == email);

    public async Task AddGuestAsync(Guest guest)
    {
        context.Guests.Add(guest);
        await context.SaveChangesAsync();
    }

    public async Task AddStaffAsync(StaffUser staff)
    {
        context.StaffUsers.Add(staff);
        await context.SaveChangesAsync();
    }

    public async Task DeleteGuestAsync(Guid id)
    {
        var guest = await context.Guests.FindAsync(id);
        if (guest != null)
        {
            context.Guests.Remove(guest);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteStaffAsync(Guid id)
    {
        var staff = await context.StaffUsers.FindAsync(id);
        if (staff != null)
        {
            context.StaffUsers.Remove(staff);
            await context.SaveChangesAsync();
        }
    }
}
