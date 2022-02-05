using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyFlashCards.Application.Behaviors;
using System.Reflection;

namespace MyFlashCards.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}
