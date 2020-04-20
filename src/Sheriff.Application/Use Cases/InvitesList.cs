using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;

namespace Sheriff.Application.UseCases
{
    public class InvitesList
    {
        private IInvitationRepository invitationRepository;

        public InvitesList(IInvitationRepository invitationRepository)
        {
            if (invitationRepository == null)
                throw new ArgumentNullException(nameof(invitationRepository));

            this.invitationRepository = invitationRepository;
        }

        public async Task<DTOs.InvitesList> Get(int banditId)
        {
            var invitations = await invitationRepository.GetFor(banditId);
            var DTOs = invitations.Select(i => i.ToDto());

            return new DTOs.InvitesList
            {
                Invitations = DTOs.ToArray()
            };
        }
    }
}