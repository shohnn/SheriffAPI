using System;
using System.Collections.Generic;

namespace Sheriff.Infrastructure.Entities
{
    public class Bandit : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int ScoringId { get; set; }
        public virtual Score Scoring  { get; set; }
        public virtual List<BandMember> Bands { get; set; }
        public virtual List<Notification> Notifications { get; set; }
    }
}