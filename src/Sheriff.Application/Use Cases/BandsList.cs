using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;

namespace Sheriff.Application.UseCases
{
    public class BandsList
    {
        private IBandRepository bandRepository;

        public BandsList(IBandRepository bandRepository)
        {
            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            this.bandRepository = bandRepository;
        }

        public async Task<DTOs.BandsList> Get(string filterName = "")
        {
            var bands = await bandRepository.Get();

            if (!string.IsNullOrEmpty(filterName))
                bands = bands.Where(b => b.Name.Contains(filterName));
            
            var DTOs = bands.Select(b => b.ToDto());

            return new DTOs.BandsList
            {
                Bands = DTOs.ToArray()
            };
        }
    }
}