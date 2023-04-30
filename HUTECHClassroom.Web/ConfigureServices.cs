using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using HUTECHClassroom.Infrastructure.Services;

namespace HUTECHClassroom.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddControllersWithViews();

        #region Services
        services.AddHttpContextAccessor();
        services.AddScoped<IExcelServie, ExcelSerive>();
        #endregion

        services.AddRazorPages();

        return services;
    }
}
