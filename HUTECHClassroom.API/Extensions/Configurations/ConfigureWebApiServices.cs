using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.API.Authorization.Missions;
using HUTECHClassroom.API.Authorization.Projects;
using HUTECHClassroom.API.Filters;
using HUTECHClassroom.API.SignalR;
using HUTECHClassroom.Persistence;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Text.Json.Serialization;

namespace HUTECHClassroom.API.Extensions.Configurations;

public static class ConfigureWebApiServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        #region Compression
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });
        #endregion

        #region Controllers
        services
            .AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        #endregion

        #region Versions
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        #endregion

        #region Authorization
        services.AddAuthorization(options =>
        {
            options.AddEntityPolicies();
        });


        //IEnumerable<Type> authorizationHandlerTypes = Assembly.GetExecutingAssembly().GetTypes()
        //    .Where(type => typeof(IAuthorizationHandler).IsAssignableFrom(type) && !type.IsInterface);

        //foreach (Type authorizationHandlerType in authorizationHandlerTypes)
        //{
        //    services.AddScoped(typeof(IAuthorizationHandler), authorizationHandlerType);
        //}

        services.AddScoped(typeof(IAuthorizationHandler), typeof(GroupRoleAuthorizationHandler));
        services.AddScoped(typeof(IAuthorizationHandler), typeof(GroupRoleFromMissionAuthorizationHandler));
        services.AddScoped(typeof(IAuthorizationHandler), typeof(GroupRoleFromProjectAuthorizationHandler));

        #endregion

        #region Swagger

        services.AddSwaggerGen(options =>
        {
            options.SchemaFilter<EnumSchemaFilter>();
            options.SchemaFilter<ExampleSchemaFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "HUTECH Classroom",
                Description = "API to manage classroom",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Slimaeus",
                    Url = new Uri("https://github.com/Slimaeus")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "HUTECH Classroom",
                Description = "API to manage classroom",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Slimaeus",
                    Url = new Uri("https://github.com/Slimaeus")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

            options.AddSecurityRequirement(securityRequirement);
        });
        #endregion

        #region Services
        services.AddEndpointsApiExplorer();
        #endregion

        #region SignalR
        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
        });
        #endregion

        #region Cors
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
            });
        });
        #endregion

        return services;
    }

    public static WebApplication UseWebApi(this WebApplication app)
    {
        #region Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "Version 2");
            options.EnableFilter();
            options.EnableTryItOutByDefault();
            options.EnablePersistAuthorization();
        });
        #endregion

        app.UseHttpsRedirection();

        #region Controllers
        app.MapControllers();
        #endregion

        #region SignalR
        app.MapHub<CommentHub>("hubs/comments");
        #endregion

        #region Authentication
        app.UseAuthentication();
        #endregion

        #region Authorization
        app.UseAuthorization();
        #endregion

        #region Compression
        app.UseResponseCompression();
        #endregion

        #region Cors
        app.UseCors();
        #endregion

        #region Redirections
        app.MapGet("", () => Results.Redirect("/swagger"))
            .ExcludeFromDescription();
        #endregion

        return app;
    }
}
