using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<UserDbContext>();

        return services;
    }
}