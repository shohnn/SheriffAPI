using System;
using System.Threading.Tasks;
using Sheriff.Domain.Models;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class CreateBand
    {
        private IBandRepository bandRepository;
        private IBanditRepository banditRepository;

        public CreateBand(IBandRepository bandRepository, IBanditRepository banditRepository)
        {
            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            if (banditRepository == null)
                throw new ArgumentNullException(nameof(banditRepository));

            this.bandRepository = bandRepository;
            this.banditRepository = banditRepository;
        }

        public async Task<DTOs.BandDetails> Create(DTOs.CreateBand request)
        {
            var boss = await banditRepository.Get(request.Boss.Id);
            if (boss == null)
                throw new NotFoundException("Bandit", request.Boss.Id);

            var band = Band.Create(request.Name, boss);

            band = await bandRepository.Add(band);
            return band.ToDtoDetails();
        }
    }
}