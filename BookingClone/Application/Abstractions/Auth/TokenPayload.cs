namespace BookingClone.Application.Abstractions.Auth;

public sealed record TokenPayload(
    Guid UserId,
    string Email,
    string FullName,
    IReadOnlyList<string> Roles);
