using MediatR;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HUTECHClassroom.API.Filters;

public sealed class ExampleSchemaFilter : ISchemaFilter
{
    private readonly IConfiguration _configuration;

    public ExampleSchemaFilter(IConfiguration configuration)
        => _configuration = configuration;
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)))
        {
            var ctorParams = context.Type
                .GetConstructors()
                .Single()
                .GetParameters()
                .Where(x => x.HasDefaultValue);

            foreach (var param in ctorParams)
            {
                var defaultValue = param.DefaultValue;

                if (param.Name is null) continue;

                var paramName = param.Name.ToLower()[0] + param.Name[1..];

                if (schema.Properties.ContainsKey(paramName))
                {
                    var existingProperty = schema.Properties[paramName];
                    existingProperty.Default = new OpenApiString(defaultValue?.ToString());
                }
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var userName = _configuration["Accounts:Admin:UserName"];
                var password = _configuration["Accounts:Admin:Password"];
                if (context.Type == typeof(LoginCommand) && userName is { } && password is { })
                {
                    schema.Example = new OpenApiObject()
                    {
                        ["userName"] = new OpenApiString(userName),
                        ["password"] = new OpenApiString(password)
                    };
                }
            }
        }
    }
}
