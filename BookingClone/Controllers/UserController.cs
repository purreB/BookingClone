using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using BookingClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    UserManager<ApplicationUser> userManager) : ControllerBase
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
        var identityDeletionResult = await DeleteIdentityUserIfExistsAsync(id);
        if (identityDeletionResult is not null)
        {
            return identityDeletionResult;
        }

        return NoContent();
    }

    [HttpDelete("staff/{id}")]
    public async Task<IActionResult> DeleteStaff(Guid id)
    {
        await userService.DeleteStaffAsync(id);
        var identityDeletionResult = await DeleteIdentityUserIfExistsAsync(id);
        if (identityDeletionResult is not null)
        {
            return identityDeletionResult;
        }

        return NoContent();
    }

    private async Task<IActionResult?> DeleteIdentityUserIfExistsAsync(Guid userId)
    {
        var identityUser = await userManager.FindByIdAsync(userId.ToString());
        if (identityUser is null)
        {
            return null;
        }

        var deleteResult = await userManager.DeleteAsync(identityUser);
        if (deleteResult.Succeeded)
        {
            return null;
        }

        var errors = deleteResult.Errors.Select(error => error.Description);
        return Problem(
            title: "Failed to delete identity user.",
            detail: string.Join("; ", errors),
            statusCode: StatusCodes.Status500InternalServerError);
    }
}
