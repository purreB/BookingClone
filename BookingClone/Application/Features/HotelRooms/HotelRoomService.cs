using AutoMapper;
using BookingClone.Application.DTOs;
using BookingClone.Domain.Hotels;
using BookingClone.Domain.Hotels.Repositories;

namespace BookingClone.Application.Features.HotelRooms;

public class HotelRoomService(IHotelRoomRepository roomRepository, IMapper mapper) : IHotelRoomService
{
    public async Task<IEnumerable<HotelRoomDto>> GetRoomsByHotelIdAsync(Guid hotelId)
    {
        var rooms = await roomRepository.GetByHotelIdAsync(hotelId);
        return mapper.Map<IEnumerable<HotelRoomDto>>(rooms);
    }

    public async Task<HotelRoomDto?> GetRoomByIdAsync(Guid id)
    {
        var room = await roomRepository.GetByIdAsync(id);
        return room == null ? null : mapper.Map<HotelRoomDto>(room);
    }

    public async Task<HotelRoomDto> AddRoomAsync(HotelRoomDto roomDto)
    {
        var room = mapper.Map<HotelRoom>(roomDto);
        room.Id = Guid.NewGuid();
        await roomRepository.AddAsync(room);
        return mapper.Map<HotelRoomDto>(room);
    }

    public async Task UpdateRoomAsync(HotelRoomDto roomDto)
    {
        var room = mapper.Map<HotelRoom>(roomDto);
        await roomRepository.UpdateAsync(room);
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        await roomRepository.DeleteAsync(id);
    }
}
