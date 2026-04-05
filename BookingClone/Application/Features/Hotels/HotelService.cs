using AutoMapper;
using BookingClone.Application.DTOs;
using BookingClone.Domain.Hotels;
using BookingClone.Domain.Hotels.Repositories;

namespace BookingClone.Application.Features.Hotels;

public class HotelService(IHotelRepository hotelRepository, IMapper mapper) : IHotelService
{
    public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
    {
        var hotels = await hotelRepository.GetAllAsync();
        return mapper.Map<IEnumerable<HotelDto>>(hotels);
    }

    public async Task<HotelDto?> GetHotelByIdAsync(Guid id)
    {
        var hotel = await hotelRepository.GetByIdAsync(id);
        return hotel == null ? null : mapper.Map<HotelDto>(hotel);
    }

    public async Task<HotelDto> AddHotelAsync(HotelDto hotelDto, Guid staffUserId)
    {
        var hotel = mapper.Map<Hotel>(hotelDto);
        hotel.Id = Guid.NewGuid();
        hotel.OwnerId = staffUserId;
        await hotelRepository.AddAsync(hotel);
        return mapper.Map<HotelDto>(hotel);
    }

    public async Task UpdateHotelAsync(HotelDto hotelDto)
    {
        var hotel = mapper.Map<Hotel>(hotelDto);
        await hotelRepository.UpdateAsync(hotel);
    }

    public async Task DeleteHotelAsync(Guid id)
    {
        await hotelRepository.DeleteAsync(id);
    }
}
