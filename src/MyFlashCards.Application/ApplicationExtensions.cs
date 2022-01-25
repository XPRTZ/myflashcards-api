using Microsoft.Extensions.DependencyInjection;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Application.Repositories;

namespace MyFlashCards.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();

        return services;
    }
}
