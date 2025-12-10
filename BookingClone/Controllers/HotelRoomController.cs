using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelRoomController(IHotelRoomService roomService) : ControllerBase
{
    [HttpGet("hotel/{hotelId}")]
    public IEnumerable<HotelRoomDto> GetRoomsByHotel(Guid hotelId) => roomService.GetRoomsByHotelId(hotelId);

    [HttpGet("{id}")]
    public ActionResult<HotelRoomDto> GetById(Guid id)
    {
        var room = roomService.GetRoomById(id);
        if (room == null) return NotFound();
        return room;
    }

    [HttpPost]
    public IActionResult Add(HotelRoomDto room)
    {
        roomService.AddRoom(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut]
    public IActionResult Update(HotelRoomDto room)
    {
        roomService.UpdateRoom(room);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        roomService.DeleteRoom(id);
        return NoContent();
    }
}
