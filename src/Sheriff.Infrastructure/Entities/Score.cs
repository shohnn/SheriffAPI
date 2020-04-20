using System;

namespace Sheriff.Infrastructure.Entities
{
    public class Score : Entity
    {
        public double LootSize { get; set; }
        public double LootValue { get; set; }
        public double Service { get; set; }
        public double Price { get; set; }
    }
}