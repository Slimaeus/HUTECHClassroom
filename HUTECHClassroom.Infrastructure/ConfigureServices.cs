using EntityFrameworkCore.UnitOfWork.Extensions;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using HUTECHClassroom.Infrastructure.Services.Authentication;
using HUTECHClassroom.Infrastructure.Services.Email;
using HUTECHClassroom.Infrastructure.Services.Excel;
using HUTECHClassroom.Infrastructure.Services.Photos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace HUTECHClassroom.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region DbContext
        services.AddDbContextPool<DbContext, ApplicationDbContext>(options =>
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ServiceConstants.TOKEN_KEY]));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        #endregion

        #region Services
        services.AddHttpContextAccessor();
        services.Configure<GmailSMTPSettings>(configuration.GetSection("EmailService:Gmail"));
        services.Configure<CloudinarySettings>(configuration.GetSection(ServiceConstants.CLOUDINARY_SETTINGS_KEY));
        services.AddScoped<IEmailService, GmailSMTPService>();
        services.AddScoped<IExcelServie, ExcelSerive>();
        services.AddScoped<IPhotoAccessor, CloudinaryPhotoAccessor>();
        #endregion

        return services;
    }
    public static async Task<WebApplication> UseInfrastructureAsync(this WebApplication app)
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
