using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class ReadNotification
    {
        private INotificationRepository notificationRepository;

        public ReadNotification(INotificationRepository notificationRepository)
        {
            if (notificationRepository == null)
                throw new ArgumentNullException(nameof(notificationRepository));

            this.notificationRepository = notificationRepository;
        }

        public async Task<DTOs.Notification> Read(int id)
        {
            var notif = await notificationRepository.Remove(id);
            if (notif == null)
                throw new NotFoundException("Notification", id);

            return notif.ToDto();
        }
    }
}