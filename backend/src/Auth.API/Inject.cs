using System.Text;
using Auth.API.Validation;
using Auth.Domain.UserManagement;
using Auth.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Auth.API;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddIdentity<User, IdentityRole>(options => 
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<UserDbContext>();
        
        var jwtSettings = configuration.GetSection("JWTSettings");
        var jwtValidIssuer = jwtSettings.GetSection("validIssuer").Value;
        var jwtValidAudience = jwtSettings.GetSection("validAudience").Value;
        var jwtSecurityKey = jwtSettings.GetSection("securityKey").Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            jwtOptions.UseSecurityTokenValidators = true;

            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtValidIssuer,
                ValidAudience = jwtValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey!))
            };

        });
        
        services.AddFluentValidationAutoValidation(options =>
        {
            options.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        return services;
    }
}