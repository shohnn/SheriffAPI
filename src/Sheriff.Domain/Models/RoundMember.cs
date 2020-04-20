using System;
using System.Linq;

namespace Sheriff.Domain.Models
{
    public class RoundMember
    {
        private RoundMember(BandMember member, Round round)
        {
            Member = member;
            Round = round;
            Scoring = Score.Zero;

            round.TrackMember(this);
        }

        public BandMember Member { get; private set; }
        public Round Round { get; private set; }
        public Score Scoring { get; private set; }
        public event EventHandler RoundScored;

        public void ScoreRound(Score score)
        {
            if (Round.Sheriff.Member.Bandit.Id == Member.Bandit.Id)
                throw new InvalidOperationException(Strings.CantScoreRound.Format(Member.Bandit.Name));

            if (Scoring != Score.Zero)
                throw new InvalidOperationException(Strings.AlreadyScored.Format(Member.Bandit.Name));

            Scoring = score;
            
            OnRoundScored(EventArgs.Empty);
        }

        protected virtual void OnRoundScored(EventArgs e)
        {
            EventHandler handler = RoundScored;
            handler?.Invoke(this, e);
        }

        public static RoundMember From(BandMember member, Round round)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (round == null)
                throw new ArgumentNullException(nameof(round));

            return new RoundMember(member, round);
        }

        public void UpdateScoring(Score score)
        {
            Scoring = score;
            Member.UpdateScoring(score);
        }
    }
}