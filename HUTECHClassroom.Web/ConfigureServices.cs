using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;

namespace HUTECHClassroom.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddControllersWithViews();

        #region Services
        #endregion

        services.AddRazorPages();

        return services;
    }
}
