using HUTECHClassroom.Application;
using HUTECHClassroom.Infrastructure;

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
        app.UseWebApi();
        await app.UseInfrastructureAsync();

        return app;
    }
}
