using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;
using Sheriff.Domain.Models;

namespace Sheriff.Infrastructure.Repositories
{
    public class AsyncEFInvitationRepository : IInvitationRepository
    {
        private SheriffContext dbContext;

        public AsyncEFInvitationRepository(SheriffContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task<Invitation> Add(Invitation invitation)
        {
            var dbInvitation = new Entities.Invitation
            {
                GuestId = invitation.Guest.Id,
                BandId = invitation.Band.Id,
                HandlerId = invitation.Handler.Id
            };

            await dbContext
                .Set<Entities.Invitation>()
                .AddAsync(dbInvitation);

            await dbContext.SaveChangesAsync();

            return Invitation.Create(dbInvitation.Id, invitation.Guest, invitation.Band, invitation.Handler);
        }

        public async Task<Invitation> Get(int id)
        {
            var dbInvitation = await dbContext
                .Set<Entities.Invitation>()
                .Include(i => i.Guest)
                .Include(i => i.Band)
                    .ThenInclude(b => b.Boss)
                        .ThenInclude(b => b.Bandit)
                .Include(i => i.Handler)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (dbInvitation == null)
                return null;

            var guest = Bandit.Create(dbInvitation.Guest.Id,
                                      dbInvitation.Guest.Name, 
                                      dbInvitation.Guest.Email);

            var handler = Bandit.Create(dbInvitation.Handler.Id,
                                        dbInvitation.Handler.Name, 
                                        dbInvitation.Handler.Email);

            var boss = Bandit.Create(dbInvitation.Band.Boss.Bandit.Id,
                                     dbInvitation.Band.Boss.Bandit.Name,
                                     dbInvitation.Band.Boss.Bandit.Email);

            var band = Band.Create(dbInvitation.Band.Id,
                                   dbInvitation.Band.Name,
                                   boss);

            var invitation = Invitation.Create(dbInvitation.Id, guest, band, handler);
            return invitation;
        }

        public async Task<IQueryable<Invitation>> GetFor(int banditId)
        {
            var dbInvitations = dbContext
                .Set<Entities.Invitation>()
                .Include(i => i.Guest)
                .Include(i => i.Band)
                    .ThenInclude(b => b.Boss)
                        .ThenInclude(b => b.Bandit)
                .Include(i => i.Handler)
                .Where(i => i.HandlerId == banditId);

            await dbInvitations.LoadAsync();

            var invitations = dbInvitations
                .AsEnumerable()
                .Select(i => {
                    var guest = Bandit.Create(i.Guest.Id,
                                              i.Guest.Name, 
                                              i.Guest.Email);

                    var handler = Bandit.Create(i.Handler.Id,
                                                i.Handler.Name, 
                                                i.Handler.Email);

                    var boss = Bandit.Create(i.Band.Boss.Bandit.Id,
                                             i.Band.Boss.Bandit.Name,
                                             i.Band.Boss.Bandit.Email);

                    var band = Band.Create(i.Band.Id,
                                           i.Band.Name,
                                           boss);

                    return Invitation.Create(i.Id, guest, band, handler);
                })
                .AsQueryable();

            return invitations;
        }

        public async Task<Invitation> Remove(int id)
        {
            var invitation = await dbContext
                .Set<Entities.Invitation>()
                .Include(i => i.Guest)
                .Include(i => i.Band)
                    .ThenInclude(b => b.Boss)
                        .ThenInclude(b => b.Bandit)
                .Include(i => i.Handler)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invitation == null)
                return null;

            var guest = Bandit.Create(invitation.Guest.Id,
                                      invitation.Guest.Name, 
                                      invitation.Guest.Email);

            var handler = Bandit.Create(invitation.Handler.Id,
                                        invitation.Handler.Name, 
                                        invitation.Handler.Email);

            var boss = Bandit.Create(invitation.Band.Boss.Bandit.Id,
                                     invitation.Band.Boss.Bandit.Name,
                                     invitation.Band.Boss.Bandit.Email);

            var band = Band.Create(invitation.Band.Id,
                                   invitation.Band.Name,
                                   boss);

            var invite = Invitation.Create(invitation.Id, guest, band, handler);

            dbContext
                .Set<Entities.Invitation>()
                .Remove(invitation);

            await dbContext.SaveChangesAsync();

            return invite;
        }
    }
}