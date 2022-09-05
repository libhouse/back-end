using System.Collections.Generic;

namespace LibHouse.Business.Notifiers
{
    public interface INotifier
    {
        bool HasNotification();
        IReadOnlyList<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}