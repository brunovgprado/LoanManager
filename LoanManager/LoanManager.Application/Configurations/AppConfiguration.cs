using AutoMapper;
using LoanManager.Application.AppServices;
using LoanManager.Application.Interfaces.AppServices;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LoanManager.Application.Configurations
{
    public class AppConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IGameAppService, GameAppService>();
            services.AddSingleton<IFriendAppService, FriendAppService>();
            services.AddSingleton<ILoanAppService, LoanAppService>();
            services.AddSingleton<IHealthCheckService, HealthCheckService>();
            return services;
        }
    }
}
