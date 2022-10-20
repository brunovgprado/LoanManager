#nullable enable
namespace LoanManager.Infrastructure.CrossCutting.NotificationContext
{
    public class Notification
    {
        public string Key { get; private set; }
        public string Message { get; private set; }
        public string? Code { get; private set; }

        public Notification(string key, string message, string? code = null)
        {
            Key = key;
            Message = message;
            Code = code;
        }
    }
}
