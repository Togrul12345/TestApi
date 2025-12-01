using Application.Common.Exceptions;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Constants;
using System.Net;
using System.Text.Json;

namespace API.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILocalizationService _localization;
        private readonly IWebHostEnvironment _environment;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILocalizationService localization, IWebHostEnvironment environment)
        {
            _next = next;
            _localization = localization;
            _environment = environment;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(DomainError.BadRequest(validationException.Failures));
                    break;
                    //case NotFoundException _:
                    //    code = HttpStatusCode.NotFound;
                    //    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string message = _environment.IsDevelopment()
                    ? exception.ToString()
                    : "server error";

                result = JsonSerializer.Serialize(DomainError.ServerError(message), options);
            }

            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
