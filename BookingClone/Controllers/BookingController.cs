using Microsoft.AspNetCore.Mvc;
using BookingClone.Application.DTOs;
using BookingClone.Application.Services;

namespace BookingClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
    {
        var bookings = await bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingDto>> Get(Guid id)
    {
        var booking = await bookingService.GetBookingByIdAsync(id);
        if (booking is null) return NotFound();
        return booking;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingDto bookingDto)
    {
        var createdBooking = await bookingService.CreateBookingAsync(bookingDto);
        return CreatedAtAction(nameof(Get), new { id = createdBooking.Id }, createdBooking);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] BookingDto bookingDto)
    {
        await bookingService.UpdateBookingAsync(bookingDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await bookingService.DeleteBookingAsync(id);
        return NoContent();
    }
}