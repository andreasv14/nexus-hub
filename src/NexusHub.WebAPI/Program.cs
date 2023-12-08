using NexusHub.Application;
using NexusHub.Infrastructure;
using NexusHub.WebAPI;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .ConfigureSwaggerAPI()
        .ConfigureCors();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("MyPolicy"); // apply the policy here
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}