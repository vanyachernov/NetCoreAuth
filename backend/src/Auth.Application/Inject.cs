using Auth.Application.Users.CreateUser;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>(); 
        
        return services;
    }
}