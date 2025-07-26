using Estudio.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Estudio.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;
            int statusCode;
            string errorMessage;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound; // 404
                    errorMessage = exception.Message;
                    break;

                case ConflictException:
                    statusCode = (int)HttpStatusCode.Conflict; // 409
                    errorMessage = exception.Message;
                    break;

                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest; // 400
                    errorMessage = exception.Message;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError; // 500
                    errorMessage = "An unexpected error occurred.";
                    break;
            }

            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = errorMessage,
                status = statusCode
            });

            return context.Response.WriteAsync(result);
        }
    }
}
