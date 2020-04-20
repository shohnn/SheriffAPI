using System;
using System.Linq;
using System.Collections.Generic;

namespace Sheriff.Domain.Models
{
    public class BandMember
    {
        private BandMember(Bandit bandit, Band band)
        {
            Bandit = bandit;
            Band = band;
            Scoring = Score.Zero;
            Rounds = new List<RoundMember>();
        }

        public Bandit Bandit { get; private set; }
        public Band Band { get; private set; }
        public List<RoundMember> Rounds { get; private set; }
        public Score Scoring { get; private set; }

        public bool CanInviteToJoin(Bandit newMember)
        {
            return Band.Boss.Bandit.Id == Bandit.Id &&
                  !Band.IsMember(newMember);
        }

        public Round CreateRound(string name, string place, DateTime dateTime)
        {
            return CreateRound(0, name, place, dateTime);
        }

        public Round CreateRound(int id, string name, string place, DateTime dateTime)
        {
            return CreateRound(id, name, place, dateTime, Enumerable.Empty<BandMember>());
        }

        public Round CreateRound(string name, string place, DateTime dateTime, IEnumerable<BandMember> members)
        {
            return CreateRound(0, name, place, dateTime, members);
        }

        public Round CreateRound(int id, string name, string place, DateTime dateTime, IEnumerable<BandMember> members)
        {
            if (!Band.CanCreateRound(this))
                throw new InvalidOperationException(Strings.CantCreateRound.Format(Bandit.Name));

            return Round.Create(id, name, place, dateTime, this, members);
        }

        public static BandMember From(Bandit bandit, Band band)
        {
            if (bandit == null)
                throw new ArgumentNullException(nameof(bandit));

            if (band == null)
                throw new ArgumentNullException(nameof(band));

            return new BandMember(bandit, band);
        }

        public void UpdateScoring(Score score)
        {
            Scoring.Add(score);
            Bandit.UpdateScoring(score);
        }
    }
}