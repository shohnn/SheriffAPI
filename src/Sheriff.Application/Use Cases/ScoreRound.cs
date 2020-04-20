using System;
using System.Linq;
using System.Threading.Tasks;
using Sheriff.Domain.Contracts;
using Sheriff.Domain.Exceptions;

namespace Sheriff.Application.UseCases
{
    public class ScoreRound
    {
        private IRoundRepository roundRepository;

        public ScoreRound(IRoundRepository roundRepository)
        {
            if (roundRepository == null)
                throw new ArgumentNullException(nameof(roundRepository));

            this.roundRepository = roundRepository;
        }

        public async Task<DTOs.MemberRound> Score(DTOs.ScoreRound request)
        {
            var round = await roundRepository.Get(request.Round.Id);
            if (round == null)
                throw new NotFoundException("Round", request.Round.Id);

            var member = round.Members.FirstOrDefault(m => m.Member.Bandit.Id == request.Member.Id);
            if (member == null)
                throw new NotFoundException("Bandit", request.Member.Id);

            member.ScoreRound(request.Score.ToModel());

            await roundRepository.UpdateScoring(member);
            
            if (round.AllScored())
                await roundRepository.UpdateScoring(round.Sheriff);

            return member.ToDtoMember();
        }
    }
}