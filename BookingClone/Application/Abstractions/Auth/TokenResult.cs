namespace BookingClone.Application.Abstractions.Auth;

public sealed record TokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc);
