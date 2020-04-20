using System;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class BanditDetails
    {
        private IBanditRepository banditRepository;

        public BanditDetails(IBanditRepository banditRepository)
        {
            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            this.banditRepository = banditRepository;
        }

        public async Task<DTOs.BanditDetails> Get(int id)
        {
            var bandit = await banditRepository.Get(id);
            if (bandit == null)
                throw new NotFoundException("Bandit", id);

            return bandit.ToDtoDetails();
        }
    }
}