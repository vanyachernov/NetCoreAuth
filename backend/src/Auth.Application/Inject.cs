using Auth.Application.Users.CreateUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}