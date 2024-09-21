using Auth.Application.Users.CreateUser;
using Auth.Application.Users.DeleteUser;
using Auth.Application.Users.GetUserList;
using Auth.Application.Users.SetDeletedStatus;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>();

        services.AddScoped<GetUserListHandler>();
        
        services.AddScoped<ChangeUserStatusHandler>();
        
        services.AddScoped<DeleteUserHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}