using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelController(IHotelService hotelService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> GetAll()
    {
        var hotels = await hotelService.GetAllHotelsAsync();
        return Ok(hotels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDto>> GetById(Guid id)
    {
        var hotel = await hotelService.GetHotelByIdAsync(id);
        if (hotel == null) return NotFound();
        return hotel;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] HotelDto hotel)
    {
        var createdHotel = await hotelService.AddHotelAsync(hotel);
        return CreatedAtAction(nameof(GetById), new { id = createdHotel.Id }, createdHotel);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] HotelDto hotel)
    {
        await hotelService.UpdateHotelAsync(hotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await hotelService.DeleteHotelAsync(id);
        return NoContent();
    }
}
