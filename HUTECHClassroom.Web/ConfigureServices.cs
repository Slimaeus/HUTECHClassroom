using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using System.Reflection;

namespace HUTECHClassroom.Web;

public static class ConfigureServices
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
                policy.RequireAssertion(context => context.User.IsInRole(RoleConstants.DEAN) || context.User.IsInRole(RoleConstants.TRAINING_OFFICE));
            });
            options.AddPolicy(TrainingOfficePolicy, policy =>
            {
                policy.RequireAssertion(context => context.User.IsInRole(RoleConstants.TRAINING_OFFICE));
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
