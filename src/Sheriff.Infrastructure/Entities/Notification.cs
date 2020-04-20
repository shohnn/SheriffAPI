using System;

namespace Sheriff.Infrastructure.Entities
{
    public class Notification : Entity
    {
        public virtual Bandit Bandit { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}