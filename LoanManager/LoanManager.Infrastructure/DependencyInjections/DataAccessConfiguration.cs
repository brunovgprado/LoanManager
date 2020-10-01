

using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Infrastructure.DataAccess.DependencyInjections
{
    public static class DataAccessConfiguration
    {
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, IUnitOfWork>();
            services.AddScoped<IGameRepository, GameRepository>();
            return services;
        }
    }
}
