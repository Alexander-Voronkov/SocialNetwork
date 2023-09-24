using Application.Common.Exceptions;
using FluentValidation;
using Newtonsoft.Json;

namespace SocialNetworkApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = "";
                if (ex is ValidationException validation)
                    message = validation.Errors.Aggregate("", (acc, err) =>
                    {
                        return acc + err.ErrorMessage + " ";
                    });
                else if (ex is BaseApiException baseApi)
                    message = baseApi.Message;
                else 
                    message = ex.Message;

                context.Response.StatusCode = 419; 
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Message = message,
                    StackTrace = ex.StackTrace,
                    Name = "ApiException"
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
