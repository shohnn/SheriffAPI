using System;
using Sheriff.Domain.Contracts;

namespace Sheriff.Domain.Notifications
{
    public class AppNotifications : Notifications
    {
        private INotificationRepository notificationRepository;

        public AppNotifications(INotificationRepository notificationRepository)
        {
            if (notificationRepository == null)
                throw new ArgumentNullException(nameof(notificationRepository));

            this.notificationRepository = notificationRepository;
        }

        protected override ISender CreateSender()
        {
            return new InApp(notificationRepository);
        }
    }
}