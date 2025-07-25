using Business.AutoMapper;
using Business.Business;
using Data.Interfaces;
using Data.Repository;
using Data.Services;
using Utilities.Custom;

namespace Web.Services
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<UserBusiness>();
            services.AddScoped<RolBusiness>();
            services.AddScoped<RolUserBusiness>();
            services.AddScoped<EncriptePassword>();

            services.AddScoped(typeof(IData<>),typeof(DataGeneric<>));
            services.AddScoped<IRolUserRepository, RolUserService>();
            //services.AddScoped<UserService>();
            services.AddScoped<IUser, UserService>();


            return services;
        }
    }
}
