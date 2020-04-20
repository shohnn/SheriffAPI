using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;

namespace Sheriff.Infrastructure.Repositories
{
    public class AsyncEFBandRepository : IBandRepository
    {
        private SheriffContext dbContext;

        public AsyncEFBandRepository(SheriffContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task<IQueryable<Band>> Get()
        {
            var dbBands = dbContext
                .Set<Entities.Band>()
                .Include(b => b.Boss)
                    .ThenInclude(bm => bm.Bandit)
                .Include(b => b.Rounds)
                    .ThenInclude(r => r.Sheriff)
                        .ThenInclude(rm => rm.Member)
                            .ThenInclude(bm => bm.Bandit);

            await dbBands.LoadAsync();

            var bands = dbBands
                .AsEnumerable()
                .Select(b => {
                    var boss = Bandit.Create(b.Boss.Bandit.Id, b.Boss.Bandit.Name, b.Boss.Bandit.Email);
                    var band = Band.Create(b.Id, b.Name, boss);

                    foreach (var dbRound in b.Rounds)
                    {
                        var dbBandit = dbRound.Sheriff.Member.Bandit;
                        var bandit = Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);

                        var sheriff = BandMember.From(bandit, band);
                        var round = Round.Create(dbRound.Id, dbRound.Name, dbRound.Place, dbRound.DateTime, sheriff, Enumerable.Empty<BandMember>());

                        band.Rounds.Add(round);
                    }

                    return band;
                })
                .AsQueryable();

            return bands;
        }

        public async Task<Band> Get(int id)
        {
            var dbBand = await dbContext
                .Set<Entities.Band>()
                .Include(b => b.Boss)
                    .ThenInclude(b => b.Bandit)
                .Include(b => b.Members)
                    .ThenInclude(bm => bm.Bandit)
                .Include(b => b.Members)
                    .ThenInclude(bm => bm.Scoring)
                .Include(b => b.Rounds)
                    .ThenInclude(r => r.Sheriff)
                        .ThenInclude(rm => rm.Member)
                            .ThenInclude(bm => bm.Bandit)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (dbBand == null)
                return null;

            var boss = Bandit.Create(dbBand.Boss.Bandit.Id, dbBand.Boss.Bandit.Name, dbBand.Boss.Bandit.Email);
            var band = Band.Create(dbBand.Id, dbBand.Name, boss);

            foreach (var dbMember in dbBand.Members)
            {
                if (dbMember.Bandit.Id == band.Boss.Bandit.Id)
                {
                    band.Members.Add(band.Boss);
                    continue;
                }

                var bandit = Bandit.Create(dbMember.Bandit.Id, dbMember.Bandit.Name, dbMember.Bandit.Email);
                var member = BandMember.From(bandit, band);

                var score = dbMember.Scoring.ToModel();
                member.Scoring.Add(score);

                band.Members.Add(member);
            }

            foreach (var dbRound in dbBand.Rounds)
            {
                var dbBandit = dbRound.Sheriff.Member.Bandit;
                var bandit = Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);

                var sheriff = BandMember.From(bandit, band);
                var round = Round.Create(dbRound.Id, dbRound.Name, dbRound.Place, dbRound.DateTime, sheriff, Enumerable.Empty<BandMember>());

                band.Rounds.Add(round);
            }

            return band;
        }

        public async Task<Band> Add(Band band)
        {
            var dbBandit = await dbContext
                .Set<Entities.Bandit>()
                .FindAsync(band.Boss.Bandit.Id);

            var dbBand = new Entities.Band
            {
                Name = band.Name
            };

            var dbBandMember = new Entities.BandMember
            {
                Band = dbBand,
                Bandit = dbBandit,
                Scoring = new Entities.Score()
            };

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await dbContext
                    .Set<Entities.Band>()
                    .AddAsync(dbBand);

                await dbContext.SaveChangesAsync();

                dbBand.Boss = dbBandMember;

                await dbContext
                    .Set<Entities.BandMember>()
                    .AddAsync(dbBandMember);

                await dbContext.SaveChangesAsync();

                ts.Complete();
            }
            
            var boss = band.Boss.Bandit;
            return Band.Create(dbBand.Id, band.Name, boss, new [] { boss });
        }

        public async Task<BandMember> AddMember(BandMember member)
        {
            var band = await dbContext
                .Set<Entities.Band>()
                .FindAsync(member.Band.Id);

            var bandit = await dbContext
                .Set<Entities.Bandit>()
                .FindAsync(member.Bandit.Id);

            var dbMember = new Entities.BandMember
            {
                Band = band,
                Bandit = bandit,
                Scoring = new Entities.Score()
            };

            await dbContext
                .Set<Entities.BandMember>()
                .AddAsync(dbMember);

            await dbContext.SaveChangesAsync();

            return member;
        }
    }
}