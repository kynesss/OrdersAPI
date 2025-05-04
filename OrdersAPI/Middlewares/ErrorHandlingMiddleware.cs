using OrdersAPI.Exceptions;

namespace OrdersAPI.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ValidationException validationEx)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(validationEx.Message);
            }
            catch (BadHttpRequestException badRequestEx)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestEx.Message);
            }
            catch (ForbidException forbidEx)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidEx.Message);
            }
            catch (NotFoundException notFoundEx)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundEx.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}