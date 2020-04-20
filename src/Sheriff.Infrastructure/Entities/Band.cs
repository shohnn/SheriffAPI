using System;
using System.Collections.Generic;

namespace Sheriff.Infrastructure.Entities
{
    public class Band : Entity
    {
        public string Name { get; set; }
        public int? BossId { get; set; }
        public BandMember Boss { get; set; }
        public virtual List<BandMember> Members { get; set; }
        public virtual List<Round> Rounds { get; set; }
    }
}