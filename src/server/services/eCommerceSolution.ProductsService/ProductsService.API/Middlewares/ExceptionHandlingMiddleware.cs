﻿using System.Net;

namespace ProductsService.API.Middlewares;


public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            if (ex.InnerException is not null)
            {
                _logger.LogError("{ExceptionType}: {ExceptionMessage}", ex.InnerException.GetType(), ex.InnerException.Message);
            }
            else
            {
                _logger.LogError("{ExceptionType}: {ExceptionMessage}", ex.GetType(), ex.Message);
            }

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(
                new
                {
                    httpContext.Response.StatusCode,
                    ex.Message,
                    Type = ex.GetType().Name
                });
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
