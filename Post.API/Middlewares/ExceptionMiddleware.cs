using Post.Application.CustomException;
using Post.Application.Logging;
using Post.Common.Models;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Post.API.Middlewares
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAppLogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, IAppLogger<ExceptionMiddleware> logger)
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
                _logger.LogError("Unhandled exception occurred: {ExceptionMessage}", ex, ex.Message);
                await HandleExceptionAsync(context, ex, _logger);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, IAppLogger<ExceptionMiddleware> _logger)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            ApiResponse<object> response;
            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.ErrorResponse(
                        message: badRequestException.Message,
                        errors: badRequestException.ValidationErrors
                    );
                    _logger.LogWarning("Bad request: {Message}", badRequestException.Message);
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = ApiResponse<object>.ErrorResponse(
                        message: notFoundException.Message
                    );
                    _logger.LogWarning("Not found: {Message}", notFoundException.Message);
                    break;
                case BadHttpRequestException badHttpRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.ErrorResponse(
                        message: badHttpRequestException.Message
                    );
                    _logger.LogWarning("Bad request: {Message}", badHttpRequestException.Message);
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.ErrorResponse(
                        message: "An unexpected error occurred."
                    );
                    _logger.LogError("Unexpected error: {ExceptionMessage}", ex, ex.Message); // ✅ Works now
                    break;
            }
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(jsonResponse);
        }
    }

}
