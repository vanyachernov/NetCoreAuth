using Auth.Application.Users;
using Auth.Domain.Shared.Interfaces;
using Auth.Infrastructure.Providers;
using Auth.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<UserDbContext>();
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}