using BookingClone.Application.DTOs;
using BookingClone.Domain;

namespace BookingClone.Application.Services;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    public HotelDto? GetHotelById(Guid id)
    {
        var hotel = hotelRepository.GetById(id);
        if (hotel == null) return null;
        return new HotelDto { Id = hotel.Id, Name = hotel.Name, Address = hotel.Address };
    }

    public IEnumerable<HotelDto> GetAllHotels()
    {
        return hotelRepository.GetAll().Select(h => new HotelDto { Id = h.Id, Name = h.Name, Address = h.Address });
    }

    public void AddHotel(HotelDto hotel)
    {
        var entity = new Hotel { Id = hotel.Id, Name = hotel.Name, Address = hotel.Address };
        hotelRepository.Add(entity);
    }

    public void UpdateHotel(HotelDto hotel)
    {
        var entity = new Hotel { Id = hotel.Id, Name = hotel.Name, Address = hotel.Address };
        hotelRepository.Update(entity);
    }

    public void DeleteHotel(Guid id) => hotelRepository.Delete(id);
}
