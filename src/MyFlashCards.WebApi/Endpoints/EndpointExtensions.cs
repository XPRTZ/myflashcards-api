using Microsoft.AspNetCore.Mvc;

namespace MyFlashCards.WebApi.Endpoints;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpoints = new List<IEndpoint>();

        foreach (var marker in scanMarkers)
        {
            endpoints.AddRange(
                marker.Assembly.ExportedTypes
                    .Where(x => typeof(IEndpoint).IsAssignableFrom(x) && !x.IsInterface)
                    .Select(Activator.CreateInstance)
                    .Cast<IEndpoint>());
        }

        services.AddSingleton(endpoints as IReadOnlyCollection<IEndpoint>);

        return services;
    }

    public static WebApplication UseEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IReadOnlyCollection<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoints(app);
        }

        return app;
    }

    public static RouteHandlerBuilder ProducesErrorCodes(this RouteHandlerBuilder builder) =>
        builder
            .Produces(StatusCodes.Status400BadRequest, typeof(ValidationProblemDetails))
            .Produces(StatusCodes.Status401Unauthorized, typeof(ProblemDetails))
            .Produces(StatusCodes.Status403Forbidden, typeof(ProblemDetails))
            .Produces(StatusCodes.Status404NotFound, typeof(ProblemDetails))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails));
}
