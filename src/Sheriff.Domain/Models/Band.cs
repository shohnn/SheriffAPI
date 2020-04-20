using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Sheriff.Domain.Models
{
    public class Band 
    {
        private Band(int id, string name, Bandit boss, IEnumerable<Bandit> members)
        {
            Id = id;
            Name = name;
            Boss = BandMember.From(boss, this);
            Members = members.Select(m => BandMember.From(m, this)).ToList();
            Rounds = new List<Round>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public BandMember Boss { get; private set; }
        public List<BandMember> Members { get; private set; }
        public List<Round> Rounds { get; private set; }

        public bool CanRequestToJoin(Bandit newMember)
        {
            return !IsMember(newMember);
        }

        public bool CanCreateRound(BandMember sheriff)
        {
            if (sheriff == null)
                throw new ArgumentNullException(nameof(sheriff));

            if (!Members.Contains(sheriff))
                return false;

            return Members.All(m => m.Rounds.Count >= sheriff.Rounds.Count);
        }

        public bool IsMember(Bandit bandit)
        {
            return Members.Any(m => m.Bandit.Id == bandit.Id);
        }

        public static Band Create(string name, Bandit boss)
        {
            return Create(0, name, boss);
        }

        public static Band Create(int id, string name, Bandit boss)
        {
            return Create(id, name, boss, Enumerable.Empty<Bandit>());
        }

        public static Band Create(int id, string name, Bandit boss, IEnumerable<Bandit> members)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(Strings.NullOrEmpty.Format(nameof(name)), nameof(name));

            if (boss == null)
                throw new ArgumentNullException(nameof(boss));

            if (members == null)
                throw new ArgumentNullException(nameof(members));

            if (members.Any(m => m == null))
                throw new ArgumentException(Strings.ContainsNull.Format(nameof(members)), nameof(members));

            return new Band(id, name, boss, members);
        }

        public string GetBandScoreTable()
        {
            StringBuilder strBuilder = new StringBuilder(); 

            strBuilder.Append(Strings.BandScores.Format(this.Name));
            this.Members.ForEach(m =>  strBuilder.Append(Strings.BandScoresTuple.Format(m.Bandit.Name, m.Scoring.GetFormatedScoreValue())));

            return strBuilder.ToString();
        }
    }
}