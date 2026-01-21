using Microsoft.AspNetCore.Mvc;
using BookingClone.Application.DTOs;
using BookingClone.Application.Services;

namespace BookingClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(IBookingService _bookingService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingService.GetAllBookings();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var booking = _bookingService.GetBookingById(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookingDto bookingDto)
        {
            _bookingService.CreateBooking(bookingDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookingDto bookingDto)
        {
            _bookingService.UpdateBooking(bookingDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _bookingService.DeleteBooking(id);
            return Ok();
        }
    }
}