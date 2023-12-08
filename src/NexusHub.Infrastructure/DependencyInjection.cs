using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusHub.Application.Services;
using NexusHub.Infrastructure.Services;

namespace NexusHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddServices()
            .AddAutoMapper(typeof(DependencyInjection).Assembly);
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}