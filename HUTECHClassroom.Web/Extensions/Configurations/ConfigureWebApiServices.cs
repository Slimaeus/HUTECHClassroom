using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Persistence;
using System.Reflection;

namespace HUTECHClassroom.Web.Extensions.Configurations;

public static class ConfigureWebApiServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Identity
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
        #endregion

        #region Web
        services.AddControllersWithViews();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(DeanOrTrainingOfficePolicy, policy =>
            {
                policy.RequireAssertion(context => context.User.IsInRole(RoleConstants.Dean) || context.User.IsInRole(RoleConstants.TrainingOffice));
            });
            options.AddPolicy(TrainingOfficePolicy, policy =>
            {
                policy.RequireAssertion(context => context.User.IsInRole(RoleConstants.TrainingOffice));
            });
        });
        services.AddRazorPages();
        #endregion

        #region Services
        #endregion

        #region Mapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        #endregion

        return services;
    }
}
