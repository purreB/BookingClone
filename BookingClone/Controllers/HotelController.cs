using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public IEnumerable<HotelDto> GetAll() => _hotelService.GetAllHotels();

    [HttpGet("{id}")]
    public ActionResult<HotelDto> GetById(Guid id)
    {
        var hotel = _hotelService.GetHotelById(id);
        if (hotel == null) return NotFound();
        return hotel;
    }

    [HttpPost]
    public IActionResult Add(HotelDto hotel)
    {
        _hotelService.AddHotel(hotel);
        return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
    }

    [HttpPut]
    public IActionResult Update(HotelDto hotel)
    {
        _hotelService.UpdateHotel(hotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _hotelService.DeleteHotel(id);
        return NoContent();
    }
}
