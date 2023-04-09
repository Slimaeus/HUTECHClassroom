using HUTECHClassroom.Infrastructure.Persistence;

namespace HUTECHClassroom.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            #region Controllers
            services.AddControllers();
            #endregion

            #region Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            #endregion

            return services;
        }
    }
}
