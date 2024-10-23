using BuildingBlocks.Exception;

public class ExceptionMiddleware(RequestDelegate next /*, ILogger logger*/)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exceptionObj)
        {
            await HandleExceptionAsync(context, exceptionObj, null!);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
    {
        var ex = exception as IException ?? exception.InnerException as IException ?? new UnexpectedException();
        context.Response.StatusCode = ex.GetStatusCode();
        return context.Response.WriteAsJsonAsync(new
        {
            ErrorTitle = ex.GetTitle(),
            ErrorValue = ex.GetResult()
        });
    }


}