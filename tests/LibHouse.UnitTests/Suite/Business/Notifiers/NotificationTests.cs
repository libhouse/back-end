using LibHouse.Business.Notifiers;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Notifiers
{
    [Collection("Business.Notifiers")]
    public class NotificationTests
    {
        [Fact]
        public void CreateNotification_ValidNotification_ShouldCreateNotification()
        {
            Notification notification = new(message: "New notification", title: "Testing Notification");
            Assert.Equal("New notification", notification.Message);
            Assert.Equal("Testing Notification", notification.Title);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("New notification", "")]
        [InlineData("", "Testing notification")]
        public void CreateNotification_InvalidNotification_ShouldNotCreateNotification(string message, string title)
        {
            Assert.ThrowsAny<Exception>(() => new Notification(message, title));
        }
    }
}