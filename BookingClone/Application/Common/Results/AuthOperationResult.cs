namespace BookingClone.Application.Common.Results;

public enum AuthErrorType
{
    Validation,
    Conflict,
    Unauthorized
}

public sealed record AuthError(AuthErrorType Type, string Message);

public sealed class AuthOperationResult<T>
{
    public T? Data { get; init; }
    public AuthError? Error { get; init; }
    public bool IsSuccess => Error is null;

    public static AuthOperationResult<T> Success(T data) =>
        new()
        {
            Data = data
        };

    public static AuthOperationResult<T> Failure(AuthErrorType type, string message) =>
        new()
        {
            Error = new AuthError(type, message)
        };
}
