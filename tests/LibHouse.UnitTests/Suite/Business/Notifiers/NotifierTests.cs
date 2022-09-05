using LibHouse.Business.Notifiers;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Notifiers
{
    public class NotifierTests
    {
        [Fact]
        public void GetNotifications_EmptyNotifications_ShouldGetEmptyList()
        {
            Notifier notifier = new();

            Assert.Empty(notifier.GetNotifications());
        }

        [Fact]
        public void GetNotifications_AddedOneNotification_ShouldGetOneNotification()
        {
            Notifier notifier = new();

            Notification notification = new("Testing notification", "First Notification");
            notifier.Handle(notification);

            Assert.Contains(notification, notifier.GetNotifications());
        }

        [Fact]
        public void Handle_TwoDistinctNotifications_ShouldHandleTwoNotifications()
        {
            Notifier notifier = new();

            Notification firstNotification = new("Testing notification", "First Notification");
            Notification secondNotification = new("Testing notification", "Second Notification");
            notifier.Handle(firstNotification);
            notifier.Handle(secondNotification);

            Assert.Contains(firstNotification, notifier.GetNotifications());
            Assert.Contains(secondNotification, notifier.GetNotifications());
        }

        [Fact]
        public void Handle_RepeatedNotifications_ShouldHandleJustOneNotification()
        {
            Notifier notifier = new();

            Notification notification = new("Testing notification", "Notification");
            notifier.Handle(notification);
            notifier.Handle(notification);

            Assert.Equal(1, notifier.GetNotifications().Count);
        }

        [Fact]
        public void HasNotification_EmptyNotifications_ShouldReturnFalse()
        {
            Notifier notifier = new();

            Assert.False(notifier.HasNotification());
        }

        [Fact]
        public void HasNotification_OneNotificationHandled_ShouldReturnTrue()
        {
            Notifier notifier = new();

            Notification notification = new("Testing notification", "First Notification");
            notifier.Handle(notification);

            Assert.True(notifier.HasNotification());
        }
    }
}