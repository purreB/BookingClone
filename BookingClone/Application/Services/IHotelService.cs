using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IHotelService
{
    HotelDto? GetHotelById(Guid id);
    IEnumerable<HotelDto> GetAllHotels();
    void AddHotel(HotelDto hotel);
    void UpdateHotel(HotelDto hotel);
    void DeleteHotel(Guid id);
}
