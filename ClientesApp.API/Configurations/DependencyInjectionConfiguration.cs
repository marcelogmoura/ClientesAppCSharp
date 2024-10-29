using ClientesApp.Domain.Interfaces.Repositories;
using ClientesApp.Domain.Interfaces.Services;
using ClientesApp.Domain.Services;
using ClientesApp.Infra.Data.Repositories;

namespace ClientesApp.API.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
        }

    }
}
