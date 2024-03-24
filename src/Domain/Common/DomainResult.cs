using System.Diagnostics.CodeAnalysis;

namespace AGInventoryManagement.Domain.Common;

public class DomainResult
{
    public DomainResult(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static DomainResult Success() => new(true, Error.None);

    public static DomainResult Failure(Error error) => new(false, error);

    public static DomainResult<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static DomainResult<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static DomainResult<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

public class DomainResult<TValue> : DomainResult
{
    private readonly TValue? _value;

    public DomainResult(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator DomainResult<TValue>(TValue? value) => Create(value);
}
