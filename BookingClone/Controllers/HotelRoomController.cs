using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelRoomController : ControllerBase
{
    private readonly IHotelRoomService _roomService;

    public HotelRoomController(IHotelRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("hotel/{hotelId}")]
    public IEnumerable<HotelRoomDto> GetRoomsByHotel(Guid hotelId) => _roomService.GetRoomsByHotelId(hotelId);

    [HttpGet("{id}")]
    public ActionResult<HotelRoomDto> GetById(Guid id)
    {
        var room = _roomService.GetRoomById(id);
        if (room == null) return NotFound();
        return room;
    }

    [HttpPost]
    public IActionResult Add(HotelRoomDto room)
    {
        _roomService.AddRoom(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut]
    public IActionResult Update(HotelRoomDto room)
    {
        _roomService.UpdateRoom(room);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _roomService.DeleteRoom(id);
        return NoContent();
    }
}
