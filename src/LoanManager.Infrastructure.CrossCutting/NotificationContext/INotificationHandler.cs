using System.Collections.Generic;

namespace LoanManager.Infrastructure.CrossCutting.NotificationContext
{
    public interface INotificationHandler
    {
        void AddNotification(Notification notification);
        void AddNotification(string key, string message);
        void AddNotification(string key, string message, string code);
        void AddNotifications(IEnumerable<Notification> notifications);
        NotificationHandler GetInstance();
    }
}
