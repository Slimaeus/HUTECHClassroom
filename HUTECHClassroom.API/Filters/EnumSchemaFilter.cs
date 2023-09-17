using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HUTECHClassroom.API.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            schema.Type = typeof(string).Name;
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(name => schema.Enum.Add(new OpenApiString($"{name}")));
        }
    }
}
