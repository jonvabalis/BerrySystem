using FluentValidation;

namespace BerrySystem.Core.Validation;

public class ExceptionMappingMiddleware(RequestDelegate next, ILogger<ExceptionMappingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errors = exception.Errors
                .Select(e => new { message = e.ErrorMessage })
                .ToList();
            await context.Response.WriteAsJsonAsync(new { errors });
        }
        catch (NotFoundException ex)
        {
            logger.LogError(ex, "Not found exception");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { errors = new[] { new { message = ex.Message } } });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(
                new { errors = new[] { new { message = "Internal Server Error" } } });
        }
    }
}