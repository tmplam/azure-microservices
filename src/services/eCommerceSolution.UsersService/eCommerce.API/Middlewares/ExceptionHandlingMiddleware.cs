using System.Net;

namespace eCommerce.API.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate _next,
    ILogger<ExceptionHandlingMiddleware> _logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.GetType()}: {ex.Message}");

            if (ex.InnerException is not null)
            {
                _logger.LogError($"{ex.InnerException.GetType()}: {ex.InnerException.Message}");
            }

            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                httpContext.Response.StatusCode,
                Type = httpContext.GetType().Name,
                ex.Message
            });
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}