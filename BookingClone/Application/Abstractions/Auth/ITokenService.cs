namespace BookingClone.Application.Abstractions.Auth;

public interface ITokenService
{
    Task<TokenResult> CreateTokenAsync(TokenPayload payload, CancellationToken cancellationToken);
}
