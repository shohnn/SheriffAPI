using System;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class BandDetails
    {
        private IBandRepository bandRepository;

        public BandDetails(IBandRepository bandRepository)
        {
            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            this.bandRepository = bandRepository;
        }

        public async Task<DTOs.BandDetails> Get(int id)
        {
            var band = await bandRepository.Get(id);
            if (band == null)
                throw new NotFoundException("Band", id);

            return band.ToDtoDetails();
        }
    }
}