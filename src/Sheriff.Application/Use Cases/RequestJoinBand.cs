using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;
using Sheriff.Domain.Models;
using Sheriff.Domain.Notifications;

namespace Sheriff.Application.UseCases
{
    public class RequestJoinBand
    {
        private IInvitationRepository invitationRepository;
        private IBanditRepository banditRepository;
        private IBandRepository bandRepository;
        private Notifications notifications;

        public RequestJoinBand(IInvitationRepository invitationRepository, IBandRepository bandRepository, IBanditRepository banditRepository, Notifications notifications)
        {
            if (invitationRepository == null)
                throw new ArgumentNullException(nameof(invitationRepository));

            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            this.notifications = notifications;
            this.bandRepository = bandRepository;
            this.banditRepository = banditRepository;
            this.invitationRepository = invitationRepository;
        }

        public async Task<DTOs.Notification> Invite(DTOs.RequestJoinBand request)
        {
            var guest = await banditRepository.Get(request.Guest.Id);
            if (guest == null)
                throw new NotFoundException("Bandit", request.Guest.Id);

            var band = await bandRepository.Get(request.Band.Id);
            if (band == null)
                throw new NotFoundException("Band", request.Band.Id);

            if (!band.CanRequestToJoin(guest))
                throw new InvalidOperationException(Strings.CantRequestToJoin.Format(guest.Name, band.Name));

            var invitation = Invitation.Create(guest, band, band.Boss.Bandit);
            await invitationRepository.Add(invitation);

            var message = Notifications.RequestJoinBand(guest.Name, band.Name);
            await notifications.Send(message, to: band.Boss.Bandit);

            return new DTOs.Notification
            {
                To = band.Boss.Bandit.Name,
                Title = message.Title,
                Content = message.Body
            };
        }
    }
}