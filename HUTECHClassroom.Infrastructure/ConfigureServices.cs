using EntityFrameworkCore.UnitOfWork.Extensions;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using HUTECHClassroom.Infrastructure.Services.Authentication;
using HUTECHClassroom.Infrastructure.Services.Email;
using HUTECHClassroom.Infrastructure.Services.Excel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace HUTECHClassroom.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region DbContext
        services.AddDbContext<DbContext, ApplicationDbContext>(options =>
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            }
            else
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));

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
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        #endregion

        #region Services
        services.AddHttpContextAccessor();
        services.Configure<GmailSMTPSettings>(configuration.GetSection("EmailService:Gmail"));
        services.AddScoped<IEmailService, GmailSMTPService>();
        services.AddScoped<IExcelServie, ExcelSerive>();
        #endregion

        return services;
    }
}
