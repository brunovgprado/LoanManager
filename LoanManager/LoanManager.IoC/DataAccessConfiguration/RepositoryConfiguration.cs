using LoanManager.Auth.Interfaces.Repository;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.IoC.DataAccessConfiguration
{
    public class RepositoryConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<IFriendRepository, FriendRepository>();
            services.AddSingleton<ILoanRepository, LoanRepository>();
            services.AddSingleton<IAuthRepository, AuthRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
