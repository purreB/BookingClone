using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IHotelRoomService
{
    HotelRoomDto? GetRoomById(Guid id);
    IEnumerable<HotelRoomDto> GetRoomsByHotelId(Guid hotelId);
    void AddRoom(HotelRoomDto room);
    void UpdateRoom(HotelRoomDto room);
    void DeleteRoom(Guid id);
}
