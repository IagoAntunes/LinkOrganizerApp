using static System.Runtime.InteropServices.JavaScript.JSType;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}
// Local: .Domain/Primitives/Result.cs

public class Result
{
    // Construtor continua protegido
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Resultado inválido.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result Failure() => new(false, Error.None); 

    public static Result<TValue> Failure<TValue>(Error error) => Result<TValue>.Failure<TValue>(error);

    public static Result<TValue> Failure<TValue>() => Result<TValue>.Failure<TValue>();
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("O valor de um resultado de falha não pode ser acessado.");

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public static new Result<TValue> Failure(Error error) => new(default, false, error);

    public static new Result<TValue> Failure() => new(default, false, Error.None);
}