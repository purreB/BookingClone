using BookingClone.Domain.Bookings;
using BookingClone.Domain.Bookings.Repositories;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Repositories.Bookings;

public class BookingRepository(BookingCloneDbContext context) : IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.HotelRoom)
            .ToListAsync();

    public async Task<Booking?> GetByIdAsync(Guid id) =>
        await context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.HotelRoom)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task AddAsync(Booking booking)
    {
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Booking booking)
    {
        booking.UpdatedAt = DateTime.UtcNow;
        context.Bookings.Update(booking);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var booking = await context.Bookings.FindAsync(id);
        if (booking != null)
        {
            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
        }
    }
}
