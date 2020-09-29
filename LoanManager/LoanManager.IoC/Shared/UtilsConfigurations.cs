using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
