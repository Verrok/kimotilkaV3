using KimotilkaV3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KimotilkaV3
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            IConfigurationBuilder configuration = new ConfigurationBuilder()
                .SetBasePath($"{env.ContentRootPath}/Configs");

            if (env.IsProduction())
            {
                configuration.AddJsonFile("appsettings.Development.json");
            } 
            else if (env.IsProduction())
            {
                configuration.AddJsonFile("appsettings.json");
            }

            AppSettings settings = new AppSettings();
            
            configuration.Build().Bind(settings);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}