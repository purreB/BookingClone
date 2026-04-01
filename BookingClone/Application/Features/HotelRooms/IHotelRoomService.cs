using BookingClone.Application.DTOs;

namespace BookingClone.Application.Features.HotelRooms;

public interface IHotelRoomService
{
    Task<IEnumerable<HotelRoomDto>> GetRoomsByHotelIdAsync(Guid hotelId);
    Task<HotelRoomDto?> GetRoomByIdAsync(Guid id);
    Task<HotelRoomDto> AddRoomAsync(HotelRoomDto room);
    Task UpdateRoomAsync(HotelRoomDto room);
    Task DeleteRoomAsync(Guid id);
}
