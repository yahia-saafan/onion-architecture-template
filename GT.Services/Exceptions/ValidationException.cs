namespace GT.Application.Exceptions;

public sealed class ValidationException(Dictionary<string, string[]> errors) 
    : BadRequestException("Validation errors occurred")
{
    public Dictionary<string, string[]> Errors { get; } = errors;
    public override string Message => FormatErrors(Errors);

    private static string FormatErrors(Dictionary<string, string[]> errors)
    {
        return string.Join("; ", errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"));
    }
}