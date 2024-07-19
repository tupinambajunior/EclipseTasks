using Microsoft.AspNetCore.Diagnostics;

namespace EclipseTasks.Api.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new ErrorDetails(exception.Message), cancellationToken);

            return true;
        }
    }

    public record ErrorDetails(string message);
}