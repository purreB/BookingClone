namespace BookingClone.Application.Abstractions.Identity;

public sealed record IdentityAccount(
    Guid Id,
    string Email,
    string FullName);
