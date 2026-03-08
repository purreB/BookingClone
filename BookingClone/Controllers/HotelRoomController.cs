using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelRoomController(IHotelRoomService roomService) : ControllerBase
{
    [HttpGet("hotel/{hotelId}")]
    public async Task<ActionResult<IEnumerable<HotelRoomDto>>> GetRoomsByHotel(Guid hotelId)
    {
        var rooms = await roomService.GetRoomsByHotelIdAsync(hotelId);
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelRoomDto>> GetById(Guid id)
    {
        var room = await roomService.GetRoomByIdAsync(id);
        if (room == null) return NotFound();
        return room;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] HotelRoomDto room)
    {
        var createdRoom = await roomService.AddRoomAsync(room);
        return CreatedAtAction(nameof(GetById), new { id = createdRoom.Id }, createdRoom);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] HotelRoomDto room)
    {
        await roomService.UpdateRoomAsync(room);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await roomService.DeleteRoomAsync(id);
        return NoContent();
    }
}
