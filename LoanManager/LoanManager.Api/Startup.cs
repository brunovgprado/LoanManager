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

namespace LoanManager.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Api GameLoans",
                        Version = "v1",
                        Description = "Loan Manager for Games",
                        Contact = new OpenApiContact
                        {
                            Name = "Bruno Prado",
                            Email = "brunomcp2010@gmail.com",
                            Url = new Uri("https://github.com/brunovitorprado")
                        }
                    });;
            });

            services.AddControllers();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IActionResultConverter, ActionResultConverter>();

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan Manager for Games");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
