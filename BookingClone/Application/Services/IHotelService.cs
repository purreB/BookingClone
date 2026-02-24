using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
    Task<HotelDto?> GetHotelByIdAsync(Guid id);
    Task<HotelDto> AddHotelAsync(HotelDto hotel);
    Task UpdateHotelAsync(HotelDto hotel);
    Task DeleteHotelAsync(Guid id);
}
