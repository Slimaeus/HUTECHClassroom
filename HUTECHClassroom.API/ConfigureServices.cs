﻿using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.API.Authorization.Missions;
using HUTECHClassroom.API.Authorization.Projects;
using HUTECHClassroom.API.Extensions;
using HUTECHClassroom.API.Filters;
using HUTECHClassroom.API.SignalR;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace HUTECHClassroom.API;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        #region Controllers
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        })
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
        #region SignalR
        app.MapHub<CommentHub>("hubs/comments");
        #endregion

        #region Cors
        app.UseCors();
        #endregion
        return app;
    }
}
