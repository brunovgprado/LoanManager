using LoanManager.IoC;
using LoanManager.Application.Configurations;
using LoanManager.IoC.DataAccessConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LoanManager.IoC.DomainConfigurations;
using LoanManager.Api.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Extensions.Configuration;
using LoanManager.Api.Configurations;

namespace LoanManager.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationConfig = configuration.Get<ApplicationConfig>();

            services.AddControllers();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IActionResultConverter, ActionResultConverter>();
            services.ConfigureSwagger();
            services.ConfigureAuthentication(applicationConfig);

            ValidatorConfiguration.ConfigureServices(services);
            AppConfiguration.ConfigureServices(services);
            DomainServicesConfiguration.ConfigureServices(services);
            RepositoryConfiguration.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerExtensions();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
