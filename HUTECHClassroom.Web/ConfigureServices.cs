namespace HUTECHClassroom.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        #region Services
        services.AddHttpContextAccessor();
        #endregion

        return services;
    }
}
