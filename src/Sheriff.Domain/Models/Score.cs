using System;
using System.Linq;

namespace Sheriff.Domain.Models
{
    public class Score
    {
        private Score()
        {
        }

        public Score(double lootSize, double lootValue, double service, double price)
        {
            LootSize = lootSize;
            LootValue = lootValue;
            Service = service;
            Price = price;
        }

        public static Score Zero => new Score();

        public double LootSize { get; private set; }
        public double LootValue { get; private set; }
        public double Service { get; private set; }
        public double Price { get; private set; }

        public void Add(Score score)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score));

            LootSize += score.LootSize;
            LootValue += score.LootValue;
            Service += score.Service;
            Price += score.Price;
        }

        public static Score Average(params Score[] scores)
        {
            double size = 0;
            double value = 0;
            double service = 0;
            double price = 0;

            foreach (var score in scores)
            {
                size += score.LootSize;
                value += score.LootValue;
                service += score.Service;
                price += score.Price;
            }

            return new Score
            {
                LootSize = size / scores.Length,
                LootValue = value / scores.Length,
                Service = service / scores.Length,
                Price = price / scores.Length
            };
        }

        public double GetFormatedScoreValue()
        {
            return (LootSize + LootValue + Service + Price) / Constants.MAX_FORMATED_SCORING_VALUE;
        }

        public override bool Equals(object obj)
        {
            return obj is Score score &&
                   score == this;
        }

        public override int GetHashCode()
        {
            int hashCode = -793958728;
            hashCode = hashCode * -1521134295 + LootSize.GetHashCode();
            hashCode = hashCode * -1521134295 + LootValue.GetHashCode();
            hashCode = hashCode * -1521134295 + Service.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Score a, Score b)
        {
            return a?.LootSize == b?.LootSize &&
                   a?.LootValue == b?.LootValue &&
                   a?.Service == b?.Service &&
                   a?.Price == b?.Price;
        }

        public static bool operator !=(Score a, Score b)
        {
            return !(a == b);
        }
    }
}