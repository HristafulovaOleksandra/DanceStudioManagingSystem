using DanceStudio.Reviews.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;
using System.Text.Json;
using FluentValidation; 

namespace DanceStudio.Reviews.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Title = "An error occurred while processing your request."
            };

            switch (exception)
            {
                case ValidationException validationEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Validation Error";
                    problemDetails.Detail = "One or more validation errors occurred.";
                    problemDetails.Extensions["errors"] = validationEx.Errors
                        .Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;

                case NotFoundException notFoundEx:
                    statusCode = (int)HttpStatusCode.NotFound;
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Detail = notFoundEx.Message;
                    break;

                case ConflictException conflictEx:
                    statusCode = (int)HttpStatusCode.Conflict;
                    problemDetails.Title = "Conflict";
                    problemDetails.Detail = conflictEx.Message;
                    break;

                case DomainException domainEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Domain Rule Violation";
                    problemDetails.Detail = domainEx.Message;
                    break;

                case MongoWriteException mongoEx when mongoEx.WriteError.Category == ServerErrorCategory.DuplicateKey:
                    statusCode = (int)HttpStatusCode.Conflict;
                    problemDetails.Title = "Duplicate Key";
                    problemDetails.Detail = "A record with the same unique key already exists.";
                    break;

                case MongoConnectionException:
                case TimeoutException:
                    statusCode = (int)HttpStatusCode.ServiceUnavailable;
                    problemDetails.Title = "Service Unavailable";
                    problemDetails.Detail = "Database is currently unreachable. Please try again later.";
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = "Internal server error.";
                    break;
            }

            problemDetails.Status = statusCode;
            context.Response.StatusCode = statusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(problemDetails, jsonOptions);

            return context.Response.WriteAsync(json);
        }
    }
}