namespace BookingClone.Application.Services;

public sealed class IdentityOperationResult
{
    private IdentityOperationResult(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    public static IdentityOperationResult Success() => new(true, null);

    public static IdentityOperationResult Failure(string errorMessage) => new(false, errorMessage);
}
