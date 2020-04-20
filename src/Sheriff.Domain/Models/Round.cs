using System;
using System.Linq;
using System.Collections.Generic;

namespace Sheriff.Domain.Models
{
    public class Round
    {
        private Round(int id, string name, string place, DateTime dateTime, BandMember sheriff, IEnumerable<BandMember> members)
        {
            Id = id;
            Name = name;
            Place = place;
            DateTime = dateTime;
            Sheriff = RoundMember.From(sheriff, this);
            Members = members.Select(m => RoundMember.From(m, this)).ToList();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Place { get; private set; }
        public DateTime DateTime { get; private set; }
        public RoundMember Sheriff { get; private set; }
        public List<RoundMember> Members { get; private set; }

        public static Round Create(int id, string name, string place, DateTime dateTime, BandMember sheriff, IEnumerable<BandMember> members)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(name)), nameof(name));

            if (string.IsNullOrEmpty(place))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(place)), nameof(place));

            if (sheriff == null)
                throw new ArgumentNullException(nameof(sheriff));

            if (members == null)
                throw new ArgumentNullException(nameof(members));
            
            if (members.Any(m => m == null))
                throw new ArgumentException(Strings.ContainsNull.Format(nameof(members)), nameof(members));

            if (members.Any(m => m.Band != sheriff.Band))
                throw new ArgumentException(Strings.ShouldBeInSameBand, nameof(members));

            return new Round(id, name, place, dateTime, sheriff, members);
        }

        public bool AllScored() 
        {
            return Members.All(m => m.Member.Bandit.Id == Sheriff.Member.Bandit.Id ||
                                    m.Scoring != Score.Zero);
        }

        public void TrackMember(RoundMember member)
        {
            member.RoundScored += OnRoundScored;
        }

        private void OnRoundScored(object sender, EventArgs e)
        {
            if (!AllScored())
                return;

            var roundScore = CalculateRoundScore();
            Sheriff.UpdateScoring(roundScore);
        }

        private Score CalculateRoundScore()
        {
            var scores = Members
                .Where(m => m.Member.Bandit.Id != Sheriff.Member.Bandit.Id)
                .Select(m => m.Scoring)
                .ToArray();

            return Score.Average(scores);
        }
    }
}