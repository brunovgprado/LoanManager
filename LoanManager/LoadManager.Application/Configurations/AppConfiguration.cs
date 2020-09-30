using AutoMapper;
using LoanManager.Application.AppServices;
using LoanManager.Application.Interfaces.AppServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Application.Configurations
{
    public class AppConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IGameAppService, GameAppService>();
            services.AddSingleton<IFriendAppService, FriendAppService>();
            return services;
        }
    }
}
