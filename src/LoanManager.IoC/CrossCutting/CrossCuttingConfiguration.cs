using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.IoC.CrossCutting
{
    public class CrossCuttingConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler, NotificationHandler>();
        }
    }
}
