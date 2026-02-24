using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("guest/{id}")]
    public async Task<ActionResult<GuestDto>> GetGuestById(Guid id)
    {
        var guest = await userService.GetGuestByIdAsync(id);
        if (guest == null) return NotFound();
        return guest;
    }

    [HttpGet("staff/{id}")]
    public async Task<ActionResult<StaffUserDto>> GetStaffById(Guid id)
    {
        var staff = await userService.GetStaffByIdAsync(id);
        if (staff == null) return NotFound();
        return staff;
    }

    [HttpPost("guest")]
    public async Task<IActionResult> AddGuest([FromBody] GuestDto guest)
    {
        var createdGuest = await userService.AddGuestAsync(guest);
        return CreatedAtAction(nameof(GetGuestById), new { id = createdGuest.Id }, createdGuest);
    }

    [HttpPost("staff")]
    public async Task<IActionResult> AddStaff([FromBody] StaffUserDto staff)
    {
        var createdStaff = await userService.AddStaffAsync(staff);
        return CreatedAtAction(nameof(GetStaffById), new { id = createdStaff.Id }, createdStaff);
    }

    [HttpPut("guest")]
    public async Task<IActionResult> UpdateGuest([FromBody] GuestDto guest)
    {
        await userService.UpdateGuestAsync(guest);
        return NoContent();
    }

    [HttpPut("staff")]
    public async Task<IActionResult> UpdateStaff([FromBody] StaffUserDto staff)
    {
        await userService.UpdateStaffAsync(staff);
        return NoContent();
    }

    [HttpDelete("guest/{id}")]
    public async Task<IActionResult> DeleteGuest(Guid id)
    {
        await userService.DeleteGuestAsync(id);
        return NoContent();
    }

    [HttpDelete("staff/{id}")]
    public async Task<IActionResult> DeleteStaff(Guid id)
    {
        await userService.DeleteStaffAsync(id);
        return NoContent();
    }
}
