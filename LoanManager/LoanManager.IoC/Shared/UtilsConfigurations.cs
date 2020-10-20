using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.IoC.Shared
{
    public class UtilsConfigurations
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services;
        }
    }
}
