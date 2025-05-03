using System.ComponentModel.DataAnnotations;

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
        }
    }
}