using System.Collections.Generic;
using System.Linq;

namespace LoanManager.Infrastructure.CrossCutting.NotificationContext
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications.ToList();
        public bool HasNotifications => _notifications.Any();

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotification(string key, string message)
        {
            AddNotification(key, message, null);
        }

        public void AddNotification(string key, string message, string code)
        {
            _notifications.Add(new Notification(key, message, code));
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public NotificationHandler GetInstance()
        {
            return this;
        }
    }
}
