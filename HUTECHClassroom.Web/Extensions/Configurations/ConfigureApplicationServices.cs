using FluentValidation;
using HUTECHClassroom.Application.Common.Behaviors;
using MediatR;

namespace HUTECHClassroom.Web.Extensions.Configurations;

public static class ConfigureApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Application.AssemblyReference.Assembly);
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        services.AddMemoryCache();
        return services;
    }
}
