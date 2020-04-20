using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;

namespace Sheriff.Application.UseCases
{
    public class AppNotifications
    {
        private INotificationRepository notificationRepository;

        public AppNotifications(INotificationRepository notificationRepository)
        {
            if (notificationRepository == null)
                throw new ArgumentNullException(nameof(notificationRepository));

            this.notificationRepository = notificationRepository;
        }

        public async Task<DTOs.AppNotifications> Get(int banditId)
        {
            var notifications = await notificationRepository.GetFor(banditId);
            var DTOs = notifications.Select(n => n.ToDto());

            return new DTOs.AppNotifications
            {
                Notifications = DTOs.ToArray()
            };
        }
    }
}