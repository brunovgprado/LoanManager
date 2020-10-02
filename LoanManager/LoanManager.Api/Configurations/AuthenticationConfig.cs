using LoanManager.Application.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Api.Configurations
{
    public static class AuthenticationConfig
    {
        public static void ConfigureAuthentication(this IServiceCollection services,
    ApplicationConfig applicationConfig)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = applicationConfig.Authentication.Authority;
                    options.Audience = applicationConfig.Authentication.Audience;
                });
        }
    }
}
