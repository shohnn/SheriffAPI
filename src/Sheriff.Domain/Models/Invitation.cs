using System;

namespace Sheriff.Domain.Models
{
    public class Invitation
    {
        public Invitation(int id, Bandit guest, Band band, Bandit handler)
        {
            Id = id;
            Guest = guest;
            Band = band;
            Handler = handler;
        }

        public int Id { get; private set; }
        public Bandit Guest { get; private set; }
        public Band Band { get; private set; }
        public Bandit Handler { get; private set; }
        public Notification Notification { get; private set; }

        public Notification CreateNotification(string title, string content)
        {
            Notification = Notification.For(Handler, title, content);
            return Notification;
        }

        public static Invitation Create(Bandit guest, Band band, Bandit handler)
        {
            return Create(0, guest, band, handler);
        }

        public static Invitation Create(int id, Bandit guest, Band band, Bandit handler)
        {
            if (guest == null)
                throw new ArgumentNullException(nameof(guest));

            if (band == null)
                throw new ArgumentNullException(nameof(band));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            return new Invitation(id, guest, band, handler);
        }
    }
}