using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class CreateRound
    {
        private IRoundRepository roundRepository;
        private IBandRepository bandRepository;

        public CreateRound(IRoundRepository roundRepository, IBandRepository bandRepository)
        {
            if (roundRepository == null)
                throw new ArgumentNullException(nameof(roundRepository));

            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            this.roundRepository = roundRepository;
            this.bandRepository = bandRepository;
        }

        public async Task<DTOs.RoundDetails> Create(DTOs.CreateRound request)
        {
            var band = await bandRepository.Get(request.Band.Id);
            if (band == null)
                throw new NotFoundException("Band", request.Band.Id);

            var sheriff = band.Members
                .FirstOrDefault(m => m.Bandit.Id == request.Sheriff.Id);

            if (sheriff == null)
                throw new NotFoundException("Bandit", request.Sheriff.Id);

            var members = band.Members
                .Where(m => request.Members.Any(r => m.Bandit.Id == r.Id));

            var round = sheriff.CreateRound(request.Name, request.Place, request.DateTime, members);

            round = await roundRepository.Add(round);
            return round.ToDtoDetails();
        }
    }
}