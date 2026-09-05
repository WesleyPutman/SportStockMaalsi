using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SportStock.Api.Errors;

public class ArgumentOutOfRangeExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken ct)
    {
        if (exception is not InvalidOperationException)
        {
            return false;
        }
        var probleme = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title  = "Requête invalide",
            Detail = exception.Message
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(probleme, ct);
        return true;
    }
}