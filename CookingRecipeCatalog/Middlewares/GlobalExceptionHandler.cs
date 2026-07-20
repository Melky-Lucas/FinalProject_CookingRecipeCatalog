using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status418ImATeapot;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private async Task<ProblemDetails> MapExceptionAsync(HttpContext context, Exception exception)
        {
            ProblemDetails problemDetails;
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
                        Detail = "One or more fields are invalid.",
                        Instance = context.Request.Path,
                        Extensions = ExtractValidationErrors(appValidationException)
                    };
                    break;

                default:
                    problemDetails = new ProblemDetails
                    {
                        Type = $"{statusCodeLink}/500",
                        Title = "Internal Server Error",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = _env.IsDevelopment()
                            ? exception.Message
                            : "Critic error occurred in the server. Please contact the app admin.",
                        Instance = context.Request.Path,
                    };

                    _logger.LogError(exception, "Error 500: {Message}\nStackTrace: {StackTrace}",
                        exception.Message, exception.StackTrace);
                    break;
            }

            return problemDetails;
        }

        private static IDictionary<string, object?> ExtractValidationErrors(AppValidationException validationException)
        {
            IDictionary<string, object?> errors = new Dictionary<string, object?>();

            foreach (var error in validationException.Errors)
            {
                errors.Add(error.Key, error.Value);
            }

            return errors;
        }
    }

}
