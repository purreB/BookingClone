using Microsoft.AspNetCore.Mvc;
using BookingClone.Application.DTOs;
using BookingClone.Application.Services;

namespace BookingClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController(IBookingService _bookingService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<BookingDto> GetAll() => _bookingService.GetAllBookings();

    [HttpGet("{id}")]
    public ActionResult<BookingDto> Get(Guid id)
    {
        var booking = _bookingService.GetBookingById(id);
        if (booking == null) return NotFound();
        return booking;
    }

    [HttpPost]
    public IActionResult Add(BookingDto bookingDto)
    {
        _bookingService.CreateBooking(bookingDto);
        return CreatedAtAction(nameof(Get), new { id = bookingDto.Id }, bookingDto);
    }

    [HttpPut]
    public IActionResult Update(BookingDto bookingDto)
    {
        _bookingService.UpdateBooking(bookingDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _bookingService.DeleteBooking(id);
        return NoContent();
    }
}