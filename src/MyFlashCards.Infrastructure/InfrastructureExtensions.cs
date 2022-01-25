using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Infrastructure.Persistence;

namespace MyFlashCards.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FlashCardsContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyFlashCards")));

        services.AddScoped<IFlashCardsContext, FlashCardsContext>();

        return services;
    }
}
