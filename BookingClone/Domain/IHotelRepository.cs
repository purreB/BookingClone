namespace BookingClone.Domain;

public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllAsync();
    Task<Hotel?> GetByIdAsync(Guid id);
    Task AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Guid id);
}
