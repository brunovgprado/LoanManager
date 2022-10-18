using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.IoC.DomainConfigurations
{
    public class DomainServicesConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGameDomainService, GameDomainService>();
            services.AddScoped<IFriendDomainService, FriendDomainService>();
            services.AddScoped<ILoanDomainService, LoanDomainService>();
        }
    }
}
