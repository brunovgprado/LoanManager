using LoanManager.Infrastructure.CrossCutting.Contracts;
using LoanManager.Infrastructure.CrossCutting.Helpers;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.IoC.CrossCutting
{
    public class CrossCuttingConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificationHandler, NotificationHandler>();
            services.AddSingleton<IEnvConfiguration>(new EnvConfiguration(configuration));
        }
    }
}
