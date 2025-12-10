using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelController(IHotelService hotelService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<HotelDto> GetAll() => hotelService.GetAllHotels();

    [HttpGet("{id}")]
    public ActionResult<HotelDto> GetById(Guid id)
    {
        var hotel = hotelService.GetHotelById(id);
        if (hotel == null) return NotFound();
        return hotel;
    }

    [HttpPost]
    public IActionResult Add(HotelDto hotel)
    {
        hotelService.AddHotel(hotel);
        return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
    }

    [HttpPut]
    public IActionResult Update(HotelDto hotel)
    {
        hotelService.UpdateHotel(hotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        hotelService.DeleteHotel(id);
        return NoContent();
    }
}
