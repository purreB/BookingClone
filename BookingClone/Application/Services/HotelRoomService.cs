using BookingClone.Application.DTOs;
using BookingClone.Domain;

namespace BookingClone.Application.Services;

public class HotelRoomService(IHotelRoomRepository roomRepository) : IHotelRoomService
{
    public HotelRoomDto? GetRoomById(Guid id)
    {
        var room = roomRepository.GetById(id);
        if (room == null) return null;
        return new HotelRoomDto { Id = room.Id, RoomNumber = room.RoomNumber, Type = room.Type, Price = room.Price, IsAvailable = room.IsAvailable };
    }

    public IEnumerable<HotelRoomDto> GetRoomsByHotelId(Guid hotelId)
    {
        return roomRepository.GetByHotelId(hotelId).Select(r => new HotelRoomDto { Id = r.Id, RoomNumber = r.RoomNumber, Type = r.Type, Price = r.Price, IsAvailable = r.IsAvailable });
    }

    public void AddRoom(HotelRoomDto room)
    {
        var entity = new HotelRoom { Id = room.Id, RoomNumber = room.RoomNumber, Type = room.Type, Price = room.Price, IsAvailable = room.IsAvailable };
        roomRepository.Add(entity);
    }

    public void UpdateRoom(HotelRoomDto room)
    {
        var entity = new HotelRoom { Id = room.Id, RoomNumber = room.RoomNumber, Type = room.Type, Price = room.Price, IsAvailable = room.IsAvailable };
        roomRepository.Update(entity);
    }

    public void DeleteRoom(Guid id) => roomRepository.Delete(id);
}
