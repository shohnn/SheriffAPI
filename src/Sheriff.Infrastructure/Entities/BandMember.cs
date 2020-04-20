using System;
using System.Collections.Generic;

namespace Sheriff.Infrastructure.Entities
{
    public class BandMember : Entity
    {
        public virtual Bandit Bandit { get; set; }
        public virtual Band Band { get; set; }
        public int ScoringId { get; set; }
        public virtual Score Scoring { get; set; }
        public virtual List<RoundMember> Rounds { get; set; }
    }
}