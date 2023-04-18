using HUTECHClassroom.Application;
using HUTECHClassroom.Infrastructure;
using HUTECHClassroom.Infrastructure.Persistence;

namespace HUTECHClassroom.API.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddWebApiServices(builder.Configuration);

        return builder.Build();
    }
    public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "HUTECH_ClassroomV1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "HUTECH_ClassroomV2");
        });
        if (app.Environment.IsDevelopment())
        {

            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
