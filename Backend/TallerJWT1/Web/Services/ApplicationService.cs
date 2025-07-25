using Business.AutoMapper;
using Business.Business;
using Business.Interfaces;
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

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IPizzasService, PizzasServices>();
            services.AddScoped<IClientesService, ClienteServices>();










            services.AddScoped<EncriptePassword>();

            services.AddScoped(typeof(IData<>),typeof(DataGeneric<>));
          
            //services.AddScoped<UserService>();
       


            return services;
        }
    }
}
