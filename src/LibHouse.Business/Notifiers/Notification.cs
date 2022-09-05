using Ardalis.GuardClauses;
using System;

namespace LibHouse.Business.Notifiers
{
    public class Notification
    {
        public string Message { get; }
        public string Title { get; }

        public Notification(string message, string title)
        {
            Guard.Against.NullOrEmpty(message, nameof(message), "A mensagem é obrigatória");
            Guard.Against.NullOrEmpty(title, nameof(title), "O título é obrigatório");

            Message = message;
            Title = title;
        }

        public override string ToString() =>
            $"Notification {Title}: {Message}";

        public override bool Equals(object obj)
        {
            return obj is Notification notification &&
                   Message == notification.Message &&
                   Title == notification.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message, Title);
        }
    }
}