using Microsoft.OpenApi.Models;

namespace Clientes.API.Configurations
{
    public class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // Definindo informações da API
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ClientesApp - API para controle de clientes",
                    Description = "API para gerenciamento de clientes.", Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Marcelo Moura",
                        Email = "mgmoura@gmail.com",
                        Url = new Uri("https://www.google.com.br")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });
        }
        /// <summary>
        /// Método para executar e aplicar as configurações do Swagger
        /// </summary>
        public static void UseSwaggerConfiguration(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // Customizando o título e o tema do SwaggerUI
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "ClientesApp API v1");
                c.DocumentTitle = "ClientesApp - Controle de Clientes";
            });
        }
    }
}