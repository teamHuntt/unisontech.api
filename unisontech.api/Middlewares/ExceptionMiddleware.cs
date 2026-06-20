using System.Net;
using System.Text.Json;
using unisontech.api.Common;

namespace unisontech.api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string message;

            switch (ex)
            {
                case NotFoundException notFound:
                    statusCode = notFound.StatusCode;
                    message = notFound.Message;
                    _logger.LogWarning("Not found on {Method} {Path} — {Message}",
                        context.Request.Method,
                        context.Request.Path,
                        message);
                    break;

                case UnauthorizedException unauthorized:
                    statusCode = unauthorized.StatusCode;
                    message = unauthorized.Message;
                    _logger.LogWarning("Unauthorized on {Method} {Path} — {Message}",
                        context.Request.Method,
                        context.Request.Path,
                        message);
                    break;

                case ForbiddenException forbidden:
                    statusCode = forbidden.StatusCode;
                    message = forbidden.Message;
                    _logger.LogWarning("Forbidden on {Method} {Path} — {Message}",
                        context.Request.Method,
                        context.Request.Path,
                        message);
                    break;

                case AppException appEx:
                    statusCode = appEx.StatusCode;
                    message = appEx.Message;
                    _logger.LogWarning("App exception on {Method} {Path} — {Message}",
                        context.Request.Method,
                        context.Request.Path,
                        message);
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred. Please try again later.";
                    _logger.LogError(ex, "Unhandled exception on {Method} {Path} — {Message}",
                        context.Request.Method,
                        context.Request.Path,
                        ex.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = ApiResponse.Fail(message);
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);
        }
    }
}
