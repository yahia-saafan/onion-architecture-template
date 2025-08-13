namespace GT.Core.Results;

public class Result : IResult
{
    protected Result(bool isSuccess, string message = "", IDictionary<string, string[]>? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors!;
    }

    #region Success

    public static IResult Success() => new Result(true);
    public static IResult Success(string message) => new Result(true, message);

    public static Task<IResult> SuccessAsync() => Task.FromResult(Success());
    public static Task<IResult> SuccessAsync(string message) => Task.FromResult(Success(message));

    #endregion

    #region Fail

    public static IResult Fail() => new Result(false);
    public static IResult Fail(string message) => new Result(false, message);
    public static IResult Fail(IDictionary<string, string[]> failures, string message)
        => new Result(false, message, failures);
    public static IResult Fail(string message, IEnumerable<string> failures)
        => new Result(false, message, new Dictionary<string, string[]> { { "General", failures.ToArray() } });
    public static IResult Fail(IEnumerable<CustomError> errors)
        => new Result(false, "Error ", new Dictionary<string, string[]> { { "General", errors.Select(x => x.Description).ToArray() } });

    public static Task<IResult> FailAsync() => Task.FromResult(Fail());
    public static Task<IResult> FailAsync(string message) => Task.FromResult(Fail(message));
    public static Task<IResult> FailAsync(IEnumerable<CustomError> errors)
        => Task.FromResult(Fail(errors));
    public static Task<IResult> FailAsync(string message, IDictionary<string, string[]> failures)
        => Task.FromResult(Fail(failures, message));
    public static Task<IResult> FailAsync(string message, IEnumerable<string> failures)
        => Task.FromResult(Fail(message, failures));

    #endregion

    #region NotFound

    public static IResult NotFound(string name, object key)
        => new Result(false, $"Entity \"{name}\" ({key}) was not found.");

    public static Task<IResult> NotFoundAsync(string name, object key)
        => Task.FromResult(NotFound(name, key));

    #endregion

    #region Properties

    public string Message { get; }
    public bool IsSuccess { get; }
    public IDictionary<string, string[]> Errors { get; }

    #endregion
}

public class Result<T> : Result, IResult<T>
{

    protected Result(bool isSuccess, string message = "", IDictionary<string, string[]>? errors = null, T data = default!)
        : base(isSuccess, message, errors)
    {
        Data = data;
    }

    public T Data { get; }

    public new static IResult<T> Success() => new Result<T>(true);
    public new static IResult<T> Success(string message) => new Result<T>(true, message);
    public static IResult<T> Success(T data) => new Result<T>(true, "Completed Successfully", data: data);
    public static IResult<T> Success(T data, string message) => new Result<T>(true, message, data: data);

    public new static Task<IResult<T>> SuccessAsync() => Task.FromResult(Success());
    public new static Task<IResult<T>> SuccessAsync(string message) => Task.FromResult(Success(message));
    public static Task<IResult<T>> SuccessAsync(T data) => Task.FromResult(Success(data));
    public static Task<IResult<T>> SuccessAsync(T data, string message) => Task.FromResult(Success(data, message));

    public new static IResult<T> Fail() => new Result<T>(false);
    public static IResult<T> Fail(T data) => new Result<T>(false, data: data);
    public new static IResult<T> Fail(string message) => new Result<T>(false, message);
    public new static IResult<T> Fail(IDictionary<string, string[]> failures, string message)
        => new Result<T>(false, message, failures);

    public new static Task<IResult<T>> FailAsync() => Task.FromResult(Fail());
    public static Task<IResult<T>> FailAsync(T data) => Task.FromResult(Fail(data));
    public new static Task<IResult<T>> FailAsync(string message) => Task.FromResult(Fail(message));
    public static Task<IResult<T>> FailAsync(IDictionary<string, string[]> failures, string message)
        => Task.FromResult(Fail(failures, message));
}