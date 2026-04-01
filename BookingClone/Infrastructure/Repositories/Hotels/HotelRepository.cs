using BookingClone.Domain.Hotels;
using BookingClone.Domain.Hotels.Repositories;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Repositories.Hotels;

public class HotelRepository(BookingCloneDbContext context) : IHotelRepository
{
    public async Task<IEnumerable<Hotel>> GetAllAsync() =>
        await context.Hotels.ToListAsync();

    public async Task<Hotel?> GetByIdAsync(Guid id) =>
        await context.Hotels.FirstOrDefaultAsync(h => h.Id == id);

    public async Task AddAsync(Hotel hotel)
    {
        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        context.Hotels.Update(hotel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var hotel = await context.Hotels.FindAsync(id);
        if (hotel != null)
        {
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
        }
    }
}
