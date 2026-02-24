namespace BookingClone.Domain;

public interface IHotelRoomRepository
{
    Task<IEnumerable<HotelRoom>> GetByHotelIdAsync(Guid hotelId);
    Task<HotelRoom?> GetByIdAsync(Guid id);
    Task AddAsync(HotelRoom room);
    Task UpdateAsync(HotelRoom room);
    Task DeleteAsync(Guid id);
}
