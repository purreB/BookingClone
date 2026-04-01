using BookingClone.Application.Common.Results;
using BookingClone.Application.DTOs;
using BookingClone.Application.Features.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Presentation.Controllers.Auth;

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
            AuthErrorType.Validation => Problem(
                title: "Validation failed.",
                detail: error.Message,
                statusCode: StatusCodes.Status400BadRequest),
            AuthErrorType.Conflict => Problem(
                title: "Conflict.",
                detail: error.Message,
                statusCode: StatusCodes.Status409Conflict),
            AuthErrorType.Unauthorized => Problem(
                title: "Unauthorized.",
                detail: error.Message,
                statusCode: StatusCodes.Status401Unauthorized),
            _ => Problem(
                title: "Authentication request failed.",
                detail: error.Message,
                statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}
