using BookingClone.Application.DTOs;
using BookingClone.Domain;

namespace BookingClone.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public HotelDto? GetHotelById(Guid id)
    {
        var hotel = _hotelRepository.GetById(id);
        if (hotel == null) return null;
        return new HotelDto { Id = hotel.Id, Name = hotel.Name, Address = hotel.Address };
    }

    public IEnumerable<HotelDto> GetAllHotels()
    {
        return _hotelRepository.GetAll().Select(h => new HotelDto { Id = h.Id, Name = h.Name, Address = h.Address });
    }

    public void AddHotel(HotelDto hotel)
    {
        var entity = new Hotel { Id = hotel.Id, Name = hotel.Name, Address = hotel.Address };
        _hotelRepository.Add(entity);
    }

    public void UpdateHotel(HotelDto hotel)
    {
        var entity = new Hotel { Id = hotel.Id, Name = hotel.Name, Address = hotel.Address };
        _hotelRepository.Update(entity);
    }

    public void DeleteHotel(Guid id) => _hotelRepository.Delete(id);
}
