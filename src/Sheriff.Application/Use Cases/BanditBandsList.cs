using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class BanditBandsList
    {
        private IBanditRepository banditRepository;

        public BanditBandsList(IBanditRepository banditRepository)
        {
            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            this.banditRepository = banditRepository;
        }

        public async Task<DTOs.BanditBandsList> Get(int banditId)
        {
            var bandit = await banditRepository.Get(banditId);
            if (bandit == null)
                throw new NotFoundException("Bandit", banditId);

            var bands = bandit.Bands.Select(b => b.ToDtoBand());

            return new DTOs.BanditBandsList
            {
                Bands = bands.ToArray()
            };
        }
    }
}