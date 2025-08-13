using GT.Application.Exceptions;
using GT.Core.Results;
using System.Net;
using System.Text.Json;

namespace GT.Web.Middlewares;

/// <summary>
/// Middleware for handling global exceptions in an ASP.NET Core application.
/// Catches and processes exceptions, providing standardized error responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware delegate in the pipeline.</param>
    /// <param name="logger">Logger instance for logging exceptions.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Middleware pipeline invocation to handle exceptions.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        object result;

        switch (ex)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                result = Result<Dictionary<string, string[]>>.Fail(validationException.Errors, "فشل التحقق.");
                _logger.LogWarning(validationException, "Validation failed: {Message}", validationException.Message);
                break;

            default:
                result = Result<string>.Fail("حدث خطأ أثناء تنفيذ العملية.");
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var resultAsJson = JsonSerializer.Serialize(result, options);

        await context.Response.WriteAsync(resultAsJson);
    }

}
