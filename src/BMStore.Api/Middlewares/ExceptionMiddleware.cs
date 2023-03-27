using BMStore.Application.CustomExceptions;
using FluentValidation;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BMStore.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext httpcontext)
    {
        try
        {
            await _next(httpcontext);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(httpcontext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        StringBuilder errorMessage = new();

        if (exception is ValidationException validationException)
        {
            foreach (var error in validationException.Errors)
            {
                errorMessage = ExceptionMessageSetter(errorMessage, error.ErrorMessage);
            }

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else if (exception is InvalidCredentialsException invalidCredentialsException)
        {
            errorMessage = ExceptionMessageSetter(errorMessage, invalidCredentialsException.Message);

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorMessage = ExceptionMessageSetter(errorMessage, exception.Message);
        }

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = errorMessage.ToString(),
        }.JsonSerialize());
    }

    private static StringBuilder ExceptionMessageSetter(StringBuilder errorMessage, string exceptionMessage)
    {
        errorMessage.Append(exceptionMessage);

        return errorMessage;
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public string JsonSerialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
