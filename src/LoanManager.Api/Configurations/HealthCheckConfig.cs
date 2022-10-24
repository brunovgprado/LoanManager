using LoanManager.Infrastructure.CrossCutting.Contracts;

namespace LoanManager.Api.Configurations
{
    public class HealthCheckConfig
    {
        public static void Configure(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var envConfig = serviceProvider.GetRequiredService<IEnvConfiguration>();

            services.AddHealthChecks()
                .AddNpgSql(envConfig.KV_DB_CONNECTIONSTRING);
        }
    }
}
