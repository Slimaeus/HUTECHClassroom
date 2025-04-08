using CloudinaryDotNet;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Services.Authentication;
using HUTECHClassroom.Infrastructure.Services.ComputerVision;
using HUTECHClassroom.Infrastructure.Services.Email;
using HUTECHClassroom.Infrastructure.Services.Excel;
using HUTECHClassroom.Infrastructure.Services.Photos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HUTECHClassroom.Web.Extensions.Configurations;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Authentication
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ServiceConstants.TOKEN_KEY]!));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var hasCookieToken = context.Request.Cookies.TryGetValue(AuthenticationConstants.CookieAccessToken, out var cookieToken);
                        if (hasCookieToken && cookieToken is { })
                        {
                            context.Token = cookieToken;
                            return Task.CompletedTask;
                        }
                        var hasAccessToken = context.Request.Query.TryGetValue(AuthenticationConstants.WebSocketAccessToken, out var accessToken);
                        if (hasAccessToken && accessToken is { })
                        {
                            context.Token = accessToken;
                            return Task.CompletedTask;
                        }
                        return Task.CompletedTask;
                    }
                };
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

        #region Settings
        services.Configure<GmailSMTPSettings>(configuration.GetSection("EmailService:Gmail"));
        services.Configure<CloudinarySettings>(configuration.GetSection(ServiceConstants.CLOUDINARY_SETTINGS_KEY));
        services.Configure<AzureComputerVisionSettings>(configuration.GetSection("Azure:ComputerVision"));
        #endregion

        #region Clients
        services.AddSingleton<IComputerVisionClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<AzureComputerVisionSettings>>().Value;

            return new ComputerVisionClient(new ApiKeyServiceClientCredentials(settings.Key))
            {
                Endpoint = settings.Endpoint
            };
        });

        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;

            return new Cloudinary(new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            ));
        });
        #endregion

        #region Services
        services.AddHttpContextAccessor();
        services.AddScoped<IEmailService, GmailSMTPService>();
        services.AddScoped<IExcelService, ExcelService>();
        services.AddScoped<IPhotoAccessor, CloudinaryPhotoAccessor>();
        services.AddScoped<IAzureComputerVisionService, AzureComputerVisionService>();
        #endregion

        return services;
    }
}
