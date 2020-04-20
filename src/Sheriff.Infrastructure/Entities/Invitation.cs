using System;

namespace Sheriff.Infrastructure.Entities
{
    public class Invitation : Entity
    {
        public int GuestId { get; set; }
        public virtual Bandit Guest { get; set; }
        public int BandId { get; set; }
        public virtual Band Band { get; set; }
        public int HandlerId { get; set; }
        public virtual Bandit Handler { get; set; }
    }
}