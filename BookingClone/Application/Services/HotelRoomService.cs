using BookingClone.Application.DTOs;
using BookingClone.Domain;

namespace BookingClone.Application.Services;

public class HotelRoomService : IHotelRoomService
{
    private readonly IHotelRoomRepository _roomRepository;

    public HotelRoomService(IHotelRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public HotelRoomDto? GetRoomById(Guid id)
    {
        var room = _roomRepository.GetById(id);
        if (room == null) return null;
        return new HotelRoomDto { Id = room.Id, RoomNumber = room.RoomNumber, Type = room.Type, Price = room.Price, IsAvailable = room.IsAvailable };
    }

    public IEnumerable<HotelRoomDto> GetRoomsByHotelId(Guid hotelId)
    {
        return _roomRepository.GetByHotelId(hotelId).Select(r => new HotelRoomDto { Id = r.Id, RoomNumber = r.RoomNumber, Type = r.Type, Price = r.Price, IsAvailable = r.IsAvailable });
    }

    public void AddRoom(HotelRoomDto room)
    {
        var entity = new HotelRoom { Id = room.Id, RoomNumber = room.RoomNumber, Type = room.Type, Price = room.Price, IsAvailable = room.IsAvailable };
        _roomRepository.Add(entity);
    }

    public void UpdateRoom(HotelRoomDto room)
    {
        var entity = new HotelRoom { Id = room.Id, RoomNumber = room.RoomNumber, Type = room.Type, Price = room.Price, IsAvailable = room.IsAvailable };
        _roomRepository.Update(entity);
    }

    public void DeleteRoom(Guid id) => _roomRepository.Delete(id);
}
