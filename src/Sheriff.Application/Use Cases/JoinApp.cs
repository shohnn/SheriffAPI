using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Notifications;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class JoinApp
    {
        private Notifications notifications;
        private IBanditRepository banditRepository;

        public JoinApp(Notifications notifications, IBanditRepository banditRepository)
        {
            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            this.notifications = notifications;
            this.banditRepository = banditRepository;
        }

        public async Task<DTOs.Notification> Invite(DTOs.JoinApp request)
        {
            var bandit = await banditRepository.FindByEmail(request.GuestEmail);
            if (bandit != null)
                throw new InvalidOperationException(Strings.EmailAlreadyUsed.Format(request.GuestEmail));

            var host = await banditRepository.Get(request.Host.Id);
            if (host == null)
                throw new NotFoundException("Bandit", request.Host.Id);

            bandit = Bandit
                .Create("New Bandit", request.GuestEmail);
            
            var message = Notifications.JoinApp(host.Name);

            await notifications.Send(message, to: bandit);

            return new DTOs.Notification
            {
                To = request.GuestEmail,
                Title = message.Title,
                Content = message.Body
            };
        }
    }
}