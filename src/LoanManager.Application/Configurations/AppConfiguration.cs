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
            services.AddScoped<IGameAppService, GameAppService>();
            services.AddScoped<IFriendAppService, FriendAppService>();
            services.AddScoped<ILoanAppService, LoanAppService>();
            return services;
        }
    }
}
