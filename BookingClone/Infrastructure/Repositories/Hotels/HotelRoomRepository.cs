using BookingClone.Domain.Hotels;
using BookingClone.Domain.Hotels.Repositories;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Repositories.Hotels;

public class HotelRoomRepository(BookingCloneDbContext context) : IHotelRoomRepository
{
    public async Task<IEnumerable<HotelRoom>> GetByHotelIdAsync(Guid hotelId) =>
        await context.HotelRooms.Where(r => r.HotelId == hotelId).ToListAsync();

    public async Task<HotelRoom?> GetByIdAsync(Guid id) =>
        await context.HotelRooms.FirstOrDefaultAsync(r => r.Id == id);

    public async Task AddAsync(HotelRoom room)
    {
        context.HotelRooms.Add(room);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(HotelRoom room)
    {
        context.HotelRooms.Update(room);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var room = await context.HotelRooms.FindAsync(id);
        if (room != null)
        {
            context.HotelRooms.Remove(room);
            await context.SaveChangesAsync();
        }
    }
}
