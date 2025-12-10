namespace BookingClone.Domain;

public interface IHotelRoomRepository
{
    HotelRoom? GetById(Guid id);
    IEnumerable<HotelRoom> GetByHotelId(Guid hotelId);
    void Add(HotelRoom room);
    void Update(HotelRoom room);
    void Delete(Guid id);
}
