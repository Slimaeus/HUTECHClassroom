namespace HUTECHClassroom.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
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
