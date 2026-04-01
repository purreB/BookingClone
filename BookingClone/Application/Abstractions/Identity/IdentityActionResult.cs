namespace BookingClone.Application.Abstractions.Identity;

public sealed class IdentityActionResult
{
    private IdentityActionResult(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    public static IdentityActionResult Success() => new(true, null);

    public static IdentityActionResult Failure(string errorMessage) => new(false, errorMessage);
}
