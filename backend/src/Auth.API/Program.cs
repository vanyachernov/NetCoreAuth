using Auth.API;
using Auth.Application;
using Auth.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddApi(configuration)
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

    app.UseAuthentication();
    
    app.UseAuthorization();
    
    app.MapControllers();
    
    app.Run();
}


