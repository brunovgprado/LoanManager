using LoanManager.Infrastructure.CrossCutting.Contracts;
using Microsoft.Extensions.Configuration;
using System;

namespace LoanManager.Infrastructure.CrossCutting.Helpers
{
    public class EnvConfiguration : IEnvConfiguration
    {
        private readonly IConfiguration _configuration;
        public string KV_DB_CONNECTIONSTRING => GetConfiguration("RdsConnectionString", true);

        public EnvConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConfiguration(string key, bool isConnectionString = false)
        {
            var envVariable = Environment.GetEnvironmentVariable(key);
            key = isConnectionString ? string.Format("{0}:{1}", "ConnectionStrings", key) : key;

            envVariable ??= _configuration.GetSection(key).Value;

            return envVariable ?? throw new ArgumentNullException($"The configuration {key} was not found");
        }
    }
}
