using System;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;

namespace Sheriff.Domain.Notifications
{
    class InApp : ISender
    {
        private INotificationRepository notificationRepository;

        public InApp(INotificationRepository notificationRepository)
        {
            if (notificationRepository == null)
                throw new ArgumentNullException(nameof(notificationRepository));

            this.notificationRepository = notificationRepository;
        }

        public async Task Send(Message message, Bandit to)
        {
            var notif = Notification.For(to, message);
            await notificationRepository.Add(notif);
        }
    }
}