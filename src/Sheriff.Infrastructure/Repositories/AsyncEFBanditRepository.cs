using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;

namespace Sheriff.Infrastructure.Repositories
{
    public class AsyncEFBanditRepository : IBanditRepository
    {
        private SheriffContext dbContext;

        public AsyncEFBanditRepository(SheriffContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this.dbContext = dbContext;
        }

        public async Task<IQueryable<Bandit>> Get()
        {
            var dbBandits = dbContext
                .Set<Entities.Bandit>()
                .Include(b => b.Scoring);

            await dbBandits.LoadAsync();

            var bandits = dbBandits
                .AsEnumerable()
                .Select(b => {
                    var bandit = Bandit.Create(b.Id, b.Name, b.Email);
                    var score = b.Scoring.ToModel();

                    bandit.Scoring.Add(score);
                    return bandit;
                })
                .AsQueryable();

            
            return bandits;
        }

        public async Task<Bandit> Get(int id)
        {
            var dbBandit = await dbContext
                .Set<Entities.Bandit>()
                .Include(b => b.Scoring)
                .Include(b => b.Bands)
                    .ThenInclude(bm => bm.Band)
                        .ThenInclude(b => b.Boss)
                            .ThenInclude(bm => bm.Bandit)
                .Include(b => b.Bands)
                    .ThenInclude(bm => bm.Scoring)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (dbBandit == null)
                return null;

            var bandit = Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);
            var score = dbBandit.Scoring.ToModel();

            bandit.Scoring.Add(score);

            foreach (var dbBand in dbBandit.Bands)
            {
                var dbBoss = dbBand.Band.Boss.Bandit;
                var boss = Bandit.Create(dbBoss.Id, dbBoss.Name, dbBoss.Email);

                var band = Band.Create(dbBand.Band.Id, dbBand.Band.Name, boss);

                var scoring = dbBand.Scoring.ToModel();

                var bandMember = BandMember.From(bandit, band);
                bandMember.Scoring.Add(scoring);

                bandit.Bands.Add(bandMember);
            }

            return bandit;
        }

        public async Task<Bandit> FindByEmail(string email)
        {
            var dbBandit = await dbContext
                .Set<Entities.Bandit>()
                .Include(b => b.Scoring)
                .Include(b => b.Bands)
                    .ThenInclude(bm => bm.Band)
                        .ThenInclude(b => b.Boss)
                            .ThenInclude(bm => bm.Bandit)
                .Include(b => b.Bands)
                    .ThenInclude(bm => bm.Scoring)
                .FirstOrDefaultAsync(b => b.Email == email);

            if (dbBandit == null)
                return null;

            var bandit = Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);
            var score = dbBandit.Scoring.ToModel();

            bandit.Scoring.Add(score);

            foreach (var dbBand in dbBandit.Bands)
            {
                var dbBoss = dbBand.Band.Boss.Bandit;
                var boss = Bandit.Create(dbBoss.Id, dbBoss.Name, dbBoss.Email);

                var band = Band.Create(dbBand.Band.Id, dbBand.Band.Name, boss);

                var scoring = dbBand.Scoring.ToModel();

                var bandMember = BandMember.From(bandit, band);
                bandMember.Scoring.Add(scoring);

                bandit.Bands.Add(bandMember);
            }

            return bandit;
        }

        public async Task<Bandit> Add(Bandit bandit)
        {
            var dbBandit = new Entities.Bandit
            {
                Name = bandit.Name,
                Email = bandit.Email.Address,
                Scoring = new Entities.Score()
            };

            await dbContext
                .Set<Entities.Bandit>()
                .AddAsync(dbBandit);

            await dbContext.SaveChangesAsync();

            return Bandit.Create(dbBandit.Id, dbBandit.Name, dbBandit.Email);
        }
    }
}