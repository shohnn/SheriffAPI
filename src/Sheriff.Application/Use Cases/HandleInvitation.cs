using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class HandleInvitation
    {
        private IInvitationRepository invitationRepository;
        private IBandRepository bandRepository;
        
        public HandleInvitation(IInvitationRepository invitationRepository, IBandRepository bandRepository)
        {
            if (invitationRepository == null)
                throw new ArgumentNullException(nameof(invitationRepository));

            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            this.invitationRepository = invitationRepository;
            this.bandRepository = bandRepository;
        }

        public async Task<DTOs.Invitation> Handle(DTOs.HandleInvitation request)
        {
            var invitation = await invitationRepository.Get(request.Invitation.Id);
            if (invitation == null)
                throw new NotFoundException("Invitation", request.Invitation.Id);

            await invitationRepository.Remove(invitation.Id);

            if (!request.Accept)
                return invitation.ToDto();

            var bandMember = BandMember.From(invitation.Guest, invitation.Band);
            await bandRepository.AddMember(bandMember);

            return invitation.ToDto();
        }
    }
}