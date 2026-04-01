using BookingClone.Application.DTOs;
using BookingClone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(
        [FromBody] RegisterRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request, cancellationToken);
        return ToActionResult(result);
    }

    private ActionResult<AuthResponseDto> ToActionResult(AuthOperationResult<AuthResponseDto> result)
    {
        if (result.IsSuccess && result.Data is not null)
        {
            return Ok(result.Data);
        }

        var error = result.Error;
        if (error is null)
        {
            return Problem(
                title: "Authentication request failed.",
                statusCode: StatusCodes.Status500InternalServerError);
        }

        return error.Type switch
        {
            AuthErrorType.Validation => BadRequest(error.Message),
            AuthErrorType.Conflict => Conflict(error.Message),
            AuthErrorType.Unauthorized => Unauthorized(error.Message),
            _ => Problem(
                title: "Authentication request failed.",
                detail: error.Message,
                statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}
