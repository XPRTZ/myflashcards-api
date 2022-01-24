using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyFlashCards.Application.Exceptions;

namespace MyFlashCards.WebApi.Exceptions;

public static class ExceptionHandlingExtensions
{
    public static void UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(c =>
            c.Run(async context =>
            {
                var exception = context.Features
                    ?.Get<IExceptionHandlerPathFeature>()
                    ?.Error;

                if (exception == null)
                {
                    return;
                }

                var response = context.Response;
                response.ContentType = "application/problem+json";

                if (exception is ValidationException validationException)
                {
                    await WriteBadRequestResponse(validationException, response);
                }
                else if (exception is NotFoundException notFoundException)
                {
                    await WriteNotFoundResponse(exception, response);
                }
                else
                {
                    await WriteInternalServerErrorResponse(exception, response);
                }
            }));
    }

    private static async Task WriteBadRequestResponse(ValidationException exception, HttpResponse response)
    {
        response.StatusCode = StatusCodes.Status400BadRequest;

        IDictionary<string, string[]> errors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(x => x.Key, x => x.Select(g => g.ErrorMessage).ToArray());

        var problem = new ValidationProblemDetails(errors)
        {
            Status = response.StatusCode,
            Title = exception.Message
        };

        await response.WriteAsJsonAsync(problem);
    }

    private static async Task WriteNotFoundResponse(Exception exception, HttpResponse response)
    {
        response.StatusCode = StatusCodes.Status404NotFound;

        var problem = new ProblemDetails
        {
            Status = response.StatusCode,
            Title = exception.Message
        };

        await response.WriteAsJsonAsync(problem);
    }

    private static async Task WriteInternalServerErrorResponse(Exception exception, HttpResponse response)
    {
        response.StatusCode = StatusCodes.Status500InternalServerError;

        var problem = new ProblemDetails
        {
            Status = response.StatusCode,
            Title = $"A problem has occurred: {exception.Message}",
            Detail = exception.ToString()
        };

        await response.WriteAsJsonAsync(problem);
    }
}
