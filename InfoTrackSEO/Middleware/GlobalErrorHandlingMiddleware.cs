using System.Net;
using Newtonsoft.Json;

namespace InfoTrackSEO.Middleware;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new
        {
            Message = "An error occurred while processing your request.",
            ExceptionMessage = exception.Message
        };

        var json = JsonConvert.SerializeObject(errorResponse);
        return context.Response.WriteAsync(json);
    }
}