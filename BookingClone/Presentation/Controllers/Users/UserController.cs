using BookingClone.Application.DTOs;
using BookingClone.Application.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Presentation.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("guest/{id}")]
    public async Task<ActionResult<GuestDto>> GetGuestById(Guid id)
    {
        var guest = await userService.GetGuestByIdAsync(id);
        if (guest == null)
        {
            return Problem(
                title: "Guest not found.",
                detail: $"No guest exists with id '{id}'.",
                statusCode: StatusCodes.Status404NotFound);
        }

        return guest;
    }

    [HttpGet("staff/{id}")]
    public async Task<ActionResult<StaffUserDto>> GetStaffById(Guid id)
    {
        var staff = await userService.GetStaffByIdAsync(id);
        if (staff == null)
        {
            return Problem(
                title: "Staff user not found.",
                detail: $"No staff user exists with id '{id}'.",
                statusCode: StatusCodes.Status404NotFound);
        }

        return staff;
    }

    [Authorize]
    [HttpDelete("guest/{id}")]
    public async Task<IActionResult> DeleteGuest(Guid id)
    {
        var result = await userService.DeleteGuestWithIdentityAsync(id);
        if (!result.IsSuccess)
        {
            return Problem(
                title: "Failed to delete identity user.",
                detail: result.ErrorMessage,
                statusCode: StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [Authorize]
    [HttpDelete("staff/{id}")]
    public async Task<IActionResult> DeleteStaff(Guid id)
    {
        var result = await userService.DeleteStaffWithIdentityAsync(id);
        if (!result.IsSuccess)
        {
            return Problem(
                title: "Failed to delete identity user.",
                detail: result.ErrorMessage,
                statusCode: StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }
}
