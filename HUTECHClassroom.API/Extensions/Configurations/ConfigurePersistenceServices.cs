using EntityFrameworkCore.UnitOfWork.Extensions;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HUTECHClassroom.API.Extensions.Configurations;

public static class ConfigurePersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region DbContext
        services.AddTriggeredDbContextPool<DbContext, ApplicationDbContext>(options =>
        {
            // Use Legacy Timestamp Behavior
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var env = Environment.GetEnvironmentVariable(ServiceConstants.ENVIRONMENT_KEY);
            if (env == ServiceConstants.DEVELOPMENT_ENVIRONMENT_VALUE)
            {
                options.UseNpgsql(configuration.GetConnectionString(ServiceConstants.CONNECTION_STRING_KEY));
            }
            else
            {
                options.UseNpgsql(configuration.GetConnectionString(ServiceConstants.CONNECTION_STRING_KEY));
            }

            options.UseTriggers(x => x.AddAssemblyTriggers());
        });

        services.AddScoped<ApplicationDbContextInitializer>();
        #endregion

        #region UnitOfWork
        services.AddUnitOfWork();
        services.AddUnitOfWork<ApplicationDbContext>();
        #endregion

        #region Identity
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        #endregion
        return services;
    }
    public static async Task<WebApplication> UsePersistenceAsync(this WebApplication app)
    {
        #region DbContext
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
        #endregion

        return app;
    }
}
