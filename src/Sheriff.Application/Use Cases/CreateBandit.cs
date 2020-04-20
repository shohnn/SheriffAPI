using System;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Models;

namespace Sheriff.Application.UseCases
{
    public class CreateBandit
    {
        private IBanditRepository banditRepository;

        public CreateBandit(IBanditRepository banditRepository)
        {
            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            this.banditRepository = banditRepository;
        }

        public async Task<DTOs.BanditDetails> Create(DTOs.CreateBandit request)
        {
            var bandit = await banditRepository.FindByEmail(request.Email);
            if (bandit != null)
                throw new InvalidOperationException(Strings.EmailAlreadyUsed.Format(request.Email));
            
            bandit = Bandit.Create(request.Name, request.Email);
            bandit = await banditRepository.Add(bandit);

            return bandit.ToDtoDetails();
        }
    }
}