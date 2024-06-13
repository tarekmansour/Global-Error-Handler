
using Microsoft.AspNetCore.Diagnostics;

namespace API;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Exception occurred: {Message}",
            exception.Message
        );

        var (statusCode, title) = MapException(exception);

        //create a problem details response with the correct status code, title, and detail.
        await Results.Problem(
            title: title,
            statusCode: statusCode,
            detail: exception.Message
        ).ExecuteAsync(httpContext);

        return true;
    }

    private static (int StatusCode, string Title) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "Bad request"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Error Server: We are on it!")
        };
    }
}
