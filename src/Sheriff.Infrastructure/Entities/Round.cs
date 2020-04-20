using System;
using System.Collections.Generic;

namespace Sheriff.Infrastructure.Entities
{
    public class Round : Entity
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public int? SheriffId { get; set; }
        public virtual RoundMember Sheriff { get; set; }
        public virtual List<RoundMember> Members { get; set; }
    }
}