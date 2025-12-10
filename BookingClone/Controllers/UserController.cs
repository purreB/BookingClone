using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("guest/{id}")]
    public ActionResult<GuestDto> GetGuestById(Guid id)
    {
        var guest = _userService.GetGuestById(id);
        if (guest == null) return NotFound();
        return guest;
    }

    [HttpGet("staff/{id}")]
    public ActionResult<StaffUserDto> GetStaffById(Guid id)
    {
        var staff = _userService.GetStaffById(id);
        if (staff == null) return NotFound();
        return staff;
    }

    [HttpPost("guest")]
    public IActionResult AddGuest(GuestDto guest)
    {
        _userService.AddGuest(guest);
        return CreatedAtAction(nameof(GetGuestById), new { id = guest.Id }, guest);
    }

    [HttpPost("staff")]
    public IActionResult AddStaff(StaffUserDto staff)
    {
        _userService.AddStaff(staff);
        return CreatedAtAction(nameof(GetStaffById), new { id = staff.Id }, staff);
    }

    [HttpPut("guest")]
    public IActionResult UpdateGuest(GuestDto guest)
    {
        _userService.UpdateGuest(guest);
        return NoContent();
    }

    [HttpPut("staff")]
    public IActionResult UpdateStaff(StaffUserDto staff)
    {
        _userService.UpdateStaff(staff);
        return NoContent();
    }

    [HttpDelete("guest/{id}")]
    public IActionResult DeleteGuest(Guid id)
    {
        _userService.DeleteGuest(id);
        return NoContent();
    }

    [HttpDelete("staff/{id}")]
    public IActionResult DeleteStaff(Guid id)
    {
        _userService.DeleteStaff(id);
        return NoContent();
    }
}
