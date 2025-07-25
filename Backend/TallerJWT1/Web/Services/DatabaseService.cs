using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Web.Services
{
    public static class DatabaseService
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(opciones => opciones
              .UseSqlServer("name=DefaultConnection"));

            return services;
        }
    }
}
