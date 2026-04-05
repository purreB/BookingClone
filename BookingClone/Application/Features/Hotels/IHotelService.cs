using BookingClone.Application.DTOs;

namespace BookingClone.Application.Features.Hotels;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
    Task<HotelDto?> GetHotelByIdAsync(Guid id);
    Task<HotelDto> AddHotelAsync(HotelDto hotel, Guid staffUserId);
    Task UpdateHotelAsync(HotelDto hotel);
    Task DeleteHotelAsync(Guid id);
}
