using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;
using System.Transactions;

namespace Sheriff.Infrastructure.Repositories
{
    public class AsyncEFRoundRepository : IRoundRepository
    {
        private SheriffContext dbContext;

        public AsyncEFRoundRepository(SheriffContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task<Round> Get(int id)
        {
            var dbRound = await dbContext
                .Set<Entities.Round>()
                .Include(r => r.Sheriff)
                    .ThenInclude(rm => rm.Scoring)
                .Include(r => r.Sheriff)
                    .ThenInclude(rm => rm.Member)
                        .ThenInclude(bm => bm.Band)
                            .ThenInclude(b => b.Boss)
                                .ThenInclude(b => b.Bandit)
                .Include(r => r.Sheriff)
                    .ThenInclude(rm => rm.Member)
                        .ThenInclude(bm => bm.Bandit)
                .Include(r => r.Members)
                    .ThenInclude(rm => rm.Scoring)
                .Include(r => r.Members)
                    .ThenInclude(rm => rm.Member)
                        .ThenInclude(bm => bm.Bandit)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (dbRound == null)
                return null;

            var dbBoss = dbRound.Sheriff.Member.Band.Boss.Bandit;
            var boss = Bandit.Create(dbBoss.Id, dbBoss.Name, dbBoss.Email);

            var dbBand = dbRound.Sheriff.Member.Band;
            var band = Band.Create(dbBand.Id, dbBand.Name, boss);

            var dbSheriff = dbRound.Sheriff.Member.Bandit;
            var sheriff = Bandit.Create(dbSheriff.Id, dbSheriff.Name, dbSheriff.Email);

            var sheriffMember = BandMember.From(sheriff, band);
            band.Members.Add(sheriffMember);

            var round = Round.Create(
                dbRound.Id,
                dbRound.Name,
                dbRound.Place,
                dbRound.DateTime,
                sheriffMember,
                Enumerable.Empty<BandMember>()
            );

            var sheriffScoring = dbRound.Sheriff.Scoring.ToModel();
            round.Sheriff.Scoring.Add(sheriffScoring);

            foreach (var rm in dbRound.Members) 
            {
                if (rm.Member.Bandit.Id == round.Sheriff.Member.Bandit.Id) {
                    round.Members.Add(round.Sheriff);
                    continue;
                }

                var dbBandit = rm.Member.Bandit;
                var bandit = Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);

                var bandMember = BandMember.From(bandit, band);
                var roundMember = RoundMember.From(bandMember, round);

                var memberScoring = rm.Scoring.ToModel();
                roundMember.Scoring.Add(memberScoring);
 
                round.Members.Add(roundMember);
            }

            return round;
        }

        public async Task<Round> Add(Round round)
        {
            var dbRound = new Entities.Round
            {
                Name = round.Name,
                Place = round.Place,
                DateTime = round.DateTime,
                Members = new List<Entities.RoundMember>()
            };

            var bandId = round.Sheriff.Member.Band.Id;
            var dbBand = await dbContext
                .Set<Entities.Band>()
                .Include(b => b.Members)
                    .ThenInclude(bm => bm.Bandit)
                .FirstOrDefaultAsync(b => b.Id == bandId);
            
            var sheriffId = round.Sheriff.Member.Bandit.Id;
            var sheriff = dbBand.Members
                .FirstOrDefault(bm => bm.Bandit.Id == sheriffId);

            var sheriffMember = new Entities.RoundMember
            {
                Member = sheriff,
                Round = dbRound,
                Scoring = new Entities.Score()
            };

            foreach (var member in round.Members)
            {
                var memberId = member.Member.Bandit.Id;
                var dbMember = dbBand.Members
                    .FirstOrDefault(bm => bm.Bandit.Id == memberId);

                var roundMember = new Entities.RoundMember
                {
                    Member = dbMember,
                    Round = dbRound,
                    Scoring = new Entities.Score()
                };

                dbRound.Members.Add(roundMember);
            }

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await dbContext
                    .Set<Entities.Round>()
                    .AddAsync(dbRound);

                dbRound.Members.ForEach(async member => {
                    await dbContext
                        .Set<Entities.RoundMember>()
                        .AddAsync(member);
                });

                await dbContext.SaveChangesAsync();

                dbRound.Sheriff = sheriffMember;
                dbBand.Rounds.Add(dbRound);

                await dbContext
                    .Set<Entities.RoundMember>()
                    .AddAsync(sheriffMember);

                await dbContext.SaveChangesAsync();

                ts.Complete();
            }

            return Round.Create(
                dbRound.Id,
                round.Name,
                round.Place,
                round.DateTime,
                round.Sheriff.Member,
                round.Members.Select(m => m.Member)
            );
        }

        public async Task<RoundMember> UpdateScoring(RoundMember roundMember)
        {
            var dbRoundMember = await dbContext
                .Set<Entities.RoundMember>()
                .Include(rm => rm.Scoring)
                .Include(rm => rm.Member)
                    .ThenInclude(bm => bm.Scoring)
                .Include(rm => rm.Member)
                    .ThenInclude(bm => bm.Bandit)
                        .ThenInclude(b => b.Scoring)
                .FirstOrDefaultAsync(rm => rm.Round.Id == roundMember.Round.Id &&
                                           rm.Member.Bandit.Id == roundMember.Member.Bandit.Id);

            if (dbRoundMember == null)
                return roundMember;

            dbRoundMember.Scoring.Update(roundMember.Scoring);
            dbRoundMember.Member.Scoring.Update(roundMember.Member.Scoring);
            dbRoundMember.Member.Bandit.Scoring.Update(roundMember.Member.Bandit.Scoring);

            await dbContext.SaveChangesAsync();

            return roundMember;
        }
    }
}