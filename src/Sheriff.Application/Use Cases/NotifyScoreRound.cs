using Sheriff.Domain.Models;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Notifications;
using Sheriff.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Sheriff.Application.UseCases
{
    public class NotifyScoreRound
    {
        private Notifications notifications;
        private IRoundRepository roundRepository;
        private IBandRepository bandRepository;

        public NotifyScoreRound(Notifications notifications, IBandRepository bandRepository, IRoundRepository roundRepository)
        {
            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            if (bandRepository == null)
                throw new ArgumentNullException(nameof(bandRepository));

            if (roundRepository == null)
                throw new ArgumentNullException(nameof(roundRepository));

            this.notifications = notifications;
            this.bandRepository = bandRepository;
            this.roundRepository = roundRepository;
        }

        public async Task<DTOs.RoundDetails> Notify(int roundId)
        {
            var round = await roundRepository.Get(roundId);

            if (round == null)
                 throw new NotFoundException("Round", roundId);

            if (!round.AllScored())
                return round.ToDtoDetails();

            var band = await bandRepository.Get(round.Sheriff.Member.Band.Id);
            
            var message = Notifications.RoundResults(band, round);

            band.Members.ForEach(async m => await notifications.Send(message, to: m.Bandit));

            return round.ToDtoDetails();
        }

    }
}