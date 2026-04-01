using BookingClone.Application.Common.Results;
using BookingClone.Application.DTOs;

namespace BookingClone.Application.Features.Auth;

public interface IAuthService
{
    Task<AuthOperationResult<AuthResponseDto>> RegisterAsync(
        RegisterRequestDto request,
        CancellationToken cancellationToken);

    Task<AuthOperationResult<AuthResponseDto>> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken);
}
