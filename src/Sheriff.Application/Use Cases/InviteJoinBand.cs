using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;
using Sheriff.Domain.Models;
using Sheriff.Domain.Notifications;

namespace Sheriff.Application.UseCases
{
    public class InviteJoinBand
    {
        private IInvitationRepository invitationRepository;
        private IBanditRepository banditRepository;
        private IBandRepository bandRepository;
        private Notifications notifications;

        public InviteJoinBand(IInvitationRepository invitationRepository, IBandRepository bandRepository, IBanditRepository banditRepository, Notifications notifications)
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

        public async Task<DTOs.Notification> Invite(DTOs.InviteJoinBand request)
        {
            var host = await banditRepository.Get(request.Host.Id);
            if (host == null)
                throw new NotFoundException("Bandit", request.Host.Id);

            var guest = await banditRepository.Get(request.Guest.Id);
            if (guest == null)
                throw new NotFoundException("Bandit", request.Guest.Id);

            var band = await bandRepository.Get(request.Band.Id);
            if (band == null)
                throw new NotFoundException("Band", request.Band.Id);
            
            if (!band.IsMember(host))
                throw new InvalidOperationException(Strings.CantInviteToJoin.Format(host.Name, guest.Name, band.Name));

            var member = BandMember.From(host, band);

            if (!member.CanInviteToJoin(guest))
                throw new InvalidOperationException(Strings.CantInviteToJoin.Format(host.Name, guest.Name, band.Name));

            var invitation = Invitation.Create(guest, band, guest);
            await invitationRepository.Add(invitation);

            var message = Notifications.InviteJoinBand(host.Name, band.Name);
            await notifications.Send(message, to: guest);

            return new DTOs.Notification
            {
                To = guest.Name,
                Title = message.Title,
                Content = message.Body
            };
        }
    }
}