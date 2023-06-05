using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using static HUTECHClassroom.Web.Extensions.Policies.PolicyConstants;

namespace HUTECHClassroom.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddControllersWithViews();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(DeanOrTrainingOfficePolicy, policy =>
            {
                policy.RequireAssertion(context => context.User.IsInRole(RoleConstants.DEAN) || context.User.IsInRole(RoleConstants.TRAINING_OFFICE));
            });
        });

        #region Services
        #endregion

        services.AddRazorPages();

        return services;
    }
}
