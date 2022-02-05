using Microsoft.Extensions.DependencyInjection;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Application.Repositories;

namespace MyFlashCards.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICardReadRepository, CardReadRepository>();
        services.AddScoped<ICardWriteRepository, CardWriteRepository>();

        services.AddScoped<ITestReadRepository, TestReadRepository>();
        services.AddScoped<ITestWriteRepository, TestWriteRepository>();

        return services;
    }
}
