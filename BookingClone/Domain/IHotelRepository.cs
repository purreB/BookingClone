namespace BookingClone.Domain;

public interface IHotelRepository
{
    Hotel? GetById(Guid id);
    IEnumerable<Hotel> GetAll();
    void Add(Hotel hotel);
    void Update(Hotel hotel);
    void Delete(Guid id);
}
