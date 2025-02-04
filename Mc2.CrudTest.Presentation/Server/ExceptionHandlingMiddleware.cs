using Mc2.CrudTest.Application.Common;
using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Presentation.Server;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AbstractDomainException ex)
        {
            await HandleExceptionResponseAsync(context, ex, StatusCodes.Status400BadRequest);
            Log(context, ex);
        }
        catch (AbstractApplicationException ex)
        {
            await HandleExceptionResponseAsync(context, ex, StatusCodes.Status400BadRequest);
            Log(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionResponseAsync(context, ex);
            Log(context, ex);
        }
    }


    private void Log(HttpContext context, Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
        logger.LogError(ex, ex.Message);
    }

    private Task HandleExceptionResponseAsync(HttpContext context, Exception ex,
        int statusCode = StatusCodes.Status500InternalServerError)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "text/plain";
        return context.Response.WriteAsync(ex.Message);
    }
}