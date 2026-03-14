using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Trader.Backend.Api.Contracts.Responses;

namespace Trader.Backend.Api.Exceptions
{
    // Global exception handler class implementing IExceptionHandler
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        // Constructor to initialize the logger
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the exception details
            _logger.LogError(exception, "An error occurred while processing your request");

            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                Title = exception.GetType().Name
            };

            // Get the status code based on the type of exception thrown
            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NoTradeFoundException:
                case TradeDoesNotExistException:
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            httpContext.Response.StatusCode = errorResponse.StatusCode;

            // Write the error response as JSON
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            // Return true to indicate that the exception was handled
            return true;
        }
    }
}
