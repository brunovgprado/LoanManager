using LoanManager.Domain.Validators.FriendValidators;
using LoanManager.Domain.Validators.GameValidators;
using LoanManager.Domain.Validators.LoanValidators;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Application.DependencyInjection
{
    public static class ValidatorConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CreateGameValidator, CreateGameValidator>();
            services.AddSingleton<CreateFriendValidator, CreateFriendValidator>();
            services.AddSingleton<CreateLoanValidator, CreateLoanValidator>();

            return services;
        }
    }
}
