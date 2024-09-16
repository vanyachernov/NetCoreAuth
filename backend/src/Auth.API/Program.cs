using Auth.Application;
using Auth.Domain.UserManagement;
using Auth.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<UserDbContext>();

    builder.Services
        .AddInfrastructure()
        .AddApplication();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}


