using FluentValidation;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System.Linq;

namespace LoanManager.Domain.DomainServices
{
    public class BaseDomainService
    {
        protected readonly INotificationHandler notificationHandler;

        protected BaseDomainService(
            INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        protected bool IsValid<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(v => new Notification("InputValidation" ,v.ErrorMessage, v.ErrorCode));
                notificationHandler.AddNotifications(errors);
                return false;
            }
            return true;
        }
    }
}
