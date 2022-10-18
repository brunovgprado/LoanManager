using LoanManager.Domain.Validators.FriendValidators;
using LoanManager.Domain.Validators.GameValidators;
using LoanManager.Domain.Validators.LoanValidators;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.IoC
{
    public static class ValidatorConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CreateGameValidator, CreateGameValidator>();
            services.AddTransient<CreateFriendValidator, CreateFriendValidator>();
            services.AddTransient<CreateLoanValidator, CreateLoanValidator>();           

            return services;
        }
    }
}
