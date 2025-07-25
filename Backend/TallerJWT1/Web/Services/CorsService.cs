

namespace Web.Services
{
    
    public static class CorsService 
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration) 
        {
            var origenesPermitidos = configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();

                });
            });
            return services;

        }

    }
}
