using System;
using Sheriff.Domain.Notifications;

namespace Sheriff.Domain.Models
{
    public class Notification
    {
        private Notification(int id, Bandit bandit, string title, string body)
        {
            Id = id;
            Bandit = bandit;
            Title = title;
            Body = body;
        }

        public int Id { get; private set; }
        public Bandit Bandit { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }

        public static Notification For(Bandit bandit, Message message)
        {
            return For(bandit, message.Title, message.Body);
        }

        public static Notification For(Bandit bandit, string title, string body)
        {
            return For(0, bandit, title, body);
        }

        public static Notification For(int id, Bandit bandit, string title, string body)
        {
            if (bandit == null)
                throw new ArgumentNullException(nameof(bandit));

            return new Notification(id, bandit, title, body);
        }
    }
}