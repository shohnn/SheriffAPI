using System;

namespace Sheriff.Infrastructure.Entities
{
    public class RoundMember : Entity
    {
        public virtual BandMember Member { get; set; }
        public virtual Round Round { get; set; }
        public int ScoringId { get; set; }
        public virtual Score Scoring { get; set; }
    }
}