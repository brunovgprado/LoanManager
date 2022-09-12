using AutoMapper;
using LoanManager.Auth.Interfaces.Services;
using LoanManager.Auth.Services;
using LoanManager.Auth.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;

namespace LoanManager.Auth.Configurations
{
    public class AuthConfigurations
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var keyHasherService = new KeyHasherService(SHA512.Create());
            services.AddSingleton(keyHasherService);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<UserValidator, UserValidator>();
            services.AddSingleton<TokenService, TokenService>();
            
            return services;
        }
    }
}
