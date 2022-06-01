using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Data;

namespace Api
{
    public class Startup
    {
        // Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            // O que precisamos para trabalhar com os controladores no projeto.
            services.AddControllers();
            // Como ja configuramos a connection string dentro de AppDbContext, não precisamos configurar mais nada.
            // Com isso, AppDbContext estará disponível em todos os métodos que a gente precisar, através de injeção de dependência.
            services.AddDbContext<AppDbContext>();
        }

        // Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Mapeia um endpoint.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
