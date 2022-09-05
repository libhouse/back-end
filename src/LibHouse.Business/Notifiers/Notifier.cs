using System.Collections.Generic;
using System.Linq;

namespace LibHouse.Business.Notifiers
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyList<Notification> GetNotifications()
        {
            return _notifications.AsReadOnly();
        }

        public void Handle(Notification notification)
        {
            if (!_notifications.Contains(notification))
            {
                _notifications.Add(notification);
            }        
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}