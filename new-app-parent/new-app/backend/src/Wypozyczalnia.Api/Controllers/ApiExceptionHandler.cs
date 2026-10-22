using Microsoft.AspNetCore.Diagnostics;
using Wypozyczalnia.Dto;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

/// <summary>Jedyne miejsce zamiany wyjatkow na odpowiedzi HTTP; tresc bledu: {"error": "..."}.</summary>
public sealed class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int status, string message) = exception switch
        {
            InvalidRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse(message), cancellationToken);
        return true;
    }
}
