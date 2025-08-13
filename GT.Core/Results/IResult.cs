namespace GT.Core.Results;

public interface IResult
{

    string Message { get; }

    bool IsSuccess { get; }

    IDictionary<string, string[]> Errors { get; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}
