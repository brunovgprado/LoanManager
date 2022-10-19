using LoanManager.Api.Configurations;
using LoanManager.Api.Configurations.Middlewares;
using LoanManager.Application.Configurations;
using LoanManager.Auth.Configurations;
using LoanManager.IoC;
using LoanManager.IoC.CrossCutting;
using LoanManager.IoC.DataAccessConfiguration;
using LoanManager.IoC.DomainConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

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

            services.AddCors();
            services.AddControllers();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureSwagger();

            AuthConfig.ConfigureAuthentication(services, this.configuration);
            ValidatorConfiguration.ConfigureServices(services);
            AppConfiguration.ConfigureServices(services);
            DomainServicesConfiguration.ConfigureServices(services);
            RepositoryConfiguration.ConfigureServices(services);
            AuthConfigurations.ConfigureServices(services);
            CrossCuttingConfiguration.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerExtensions();
            app.UseMiddleware<ExceptionHandler>();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
