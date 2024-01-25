using Microsoft.AspNetCore.Diagnostics;
using ProductApi.Exceptions;

namespace ProductApi;

public class GlobalExceptionHandler() : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCodes, errorMessage) = exception switch
        {
            UnauthorizedException => (401, exception.Message),
            NotFoundException => (404, exception.Message),
            AlreadyExistException => (409, exception.Message),
            _ => (500, "Something went wrong")
        };

        httpContext.Response.StatusCode = statusCodes;
        await httpContext.Response.WriteAsJsonAsync(errorMessage, cancellationToken);
        return true;
    }
}
