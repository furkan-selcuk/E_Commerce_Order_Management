using System.Net;
using System.Text.Json;
using ECommerce.Application.Exceptions;

namespace ECommerce.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // hata yakalama
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (NotFoundException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleException(context, "Beklenmeyen bir hata oluştu: " + ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }
        

        //hata mesajı döndürme
        private static async Task HandleException(
            HttpContext context,
            string message,
            HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
