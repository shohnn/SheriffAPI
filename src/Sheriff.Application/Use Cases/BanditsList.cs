using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;

namespace Sheriff.Application.UseCases
{
    public class BanditsList
    {
        private IBanditRepository banditRepository;

        public BanditsList(IBanditRepository banditRepository)
        {
            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            this.banditRepository = banditRepository;
        }

        public async Task<DTOs.BanditsList> Get()
        {
            var bandits = await banditRepository.Get();
            var DTOs = bandits.Select(b => b.ToDto());

            return new DTOs.BanditsList
            { 
                Bandits = DTOs.ToArray()
            };
        }
    }
}