using BookingClone.Domain;

namespace BookingClone.Infrastructure.Repositories;

public class HotelRoomRepository : IHotelRoomRepository
{
    private readonly List<HotelRoom> _rooms = new();

    public HotelRoom? GetById(Guid id) => _rooms.FirstOrDefault(r => r.Id == id);
    public IEnumerable<HotelRoom> GetByHotelId(Guid hotelId) => _rooms.Where(r => r.HotelId == hotelId);
    public void Add(HotelRoom room) => _rooms.Add(room);
    public void Update(HotelRoom room)
    {
        var idx = _rooms.FindIndex(r => r.Id == room.Id);
        if (idx >= 0) _rooms[idx] = room;
    }
    public void Delete(Guid id) => _rooms.RemoveAll(r => r.Id == id);
}
