using DanceStudioManagingSystem;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using DanceStudio.Booking.Bll.Exceptions;
namespace DanceStudioManagingSystem.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            switch (exception)
            {
                case NotFoundException ex: 
                    problemDetails.Title = "Resource not found";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Detail = ex.Message;
                    break;

                case ApplicationException ex:
                    problemDetails.Title = "Business logic error";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest; 
                    problemDetails.Detail = ex.Message;
                    break;

                default: 
                    _logger.LogError(exception, "An unhandled exception has occurred.");
                    problemDetails.Title = "An internal server error occurred";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError; 
                    problemDetails.Detail = "An unexpected error occurred. Please try again later.";
                    break;
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            var json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
    }
}