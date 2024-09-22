using System.Text;
using Auth.API.Validation;
using Auth.Domain.UserManagement;
using Auth.Infrastructure;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Auth.API;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        DotNetEnv.Env.Load();
        
        services.AddIdentity<User, IdentityRole>(options => 
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<UserDbContext>();
        
        
        var jwtSecurityKey = Env.GetString("JWT_SECRET");
        var jwtValidIssuer = Env.GetString("JWT_ISSUER");
        var jwtValidAudience = Env.GetString("JWT_AUDIENCE");

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
        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("https://net-core-auth.vercel.app/");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        return services;
    }
}