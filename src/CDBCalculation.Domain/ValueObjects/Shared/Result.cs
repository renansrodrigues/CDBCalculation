namespace CDBCalculation.Domain.ValueObjects.Shared;

public sealed record Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }

    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> SuccessValidation()
        => new(true, default, null);

    public static Result<T> Failure(string error)
        => new(false, default, error);

    public static Result<T> Success(T obj)
        => new(true, obj, null);

}
