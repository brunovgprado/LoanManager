using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.IoC.DomainConfigurations
{
    public class DomainServicesConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGameDomainService, GameDomainService>();
            services.AddSingleton<IFriendDomainService, FriendDomainService>();
            services.AddSingleton<ILoanDomainService, LoanDomainService>();
        }
    }
}
