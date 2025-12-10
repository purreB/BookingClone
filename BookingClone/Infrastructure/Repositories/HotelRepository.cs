using BookingClone.Domain;

namespace BookingClone.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly List<Hotel> _hotels = new();

    public Hotel? GetById(Guid id) => _hotels.FirstOrDefault(h => h.Id == id);
    public IEnumerable<Hotel> GetAll() => _hotels;
    public void Add(Hotel hotel) => _hotels.Add(hotel);
    public void Update(Hotel hotel)
    {
        var idx = _hotels.FindIndex(h => h.Id == hotel.Id);
        if (idx >= 0) _hotels[idx] = hotel;
    }
    public void Delete(Guid id) => _hotels.RemoveAll(h => h.Id == id);
}
