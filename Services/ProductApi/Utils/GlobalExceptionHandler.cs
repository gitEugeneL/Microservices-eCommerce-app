using Microsoft.AspNetCore.Diagnostics;

namespace ProductApi.Utils;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCodes, errorMessage) = exception switch
        {
            // [!] If you want you can catch other exceptions here
            _ => (500, "Something went wrong")
        };

        httpContext.Response.StatusCode = statusCodes;
        await httpContext.Response.WriteAsJsonAsync(errorMessage, cancellationToken);
        return true;
    }
}
