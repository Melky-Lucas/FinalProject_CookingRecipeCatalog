using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = await MapExceptionAsync(httpContext, exception);

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = problemDetails.Status;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private async Task<CustomProblemDetails> MapExceptionAsync(HttpContext context, Exception exception)
        {
            CustomProblemDetails problemDetails;
            string statusCodeLink = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status";
            int statusCode;

            switch (exception)
            {
                case AppValidationException appValidationException:
                    statusCode = appValidationException.StatusCode;

                    problemDetails = new()
                    {
                        Type = $"{statusCodeLink}/{statusCode}",
                        Title = "Validation Error",
                        Status = statusCode,
                        Detail = exception.Message,
                        Instance = context.Request.Path,
                        TraceId = context.TraceIdentifier,
                        Timestamp = DateTime.UtcNow,
                        Errors = ExtractValidationErrors(appValidationException)
                    };
                    break;

                default:
                    problemDetails = new CustomProblemDetails
                    {
                        Type = $"{statusCodeLink}/500",
                        Title = "Internal Server Error",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = _env.IsDevelopment()
                            ? exception.Message
                            : "Critic error occurred in the server. Please contact the app admin.",
                        Instance = context.Request.Path,
                        TraceId = context.TraceIdentifier,
                        Timestamp = DateTime.UtcNow
                    };

                    _logger.LogError(exception, "Error 500: {Message}\nStackTrace: {StackTrace}",
                        exception.Message, exception.StackTrace);
                    break;
            }

            return problemDetails;
        }

        private static Dictionary<string, string[]> ExtractValidationErrors(AppValidationException validationException)
        {
            Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

            foreach (var error in validationException.Errors)
            {
                errors.Add(error.Key, error.Value);
            }

            return errors;
        }
    }

}
