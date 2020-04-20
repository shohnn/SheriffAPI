using System;
using Models = Sheriff.Domain.Models;
using Entities = Sheriff.Infrastructure.Entities;

namespace Sheriff
{
    static class ScoreExtension
    {
        public static Models.Score ToModel(this Entities.Score score)
        {
            return new Models.Score(
                lootSize: score.LootSize,
                lootValue: score.LootValue,
                service: score.Service,
                price: score.Price
            );
        }

        public static void Update(this Entities.Score score, Models.Score newScore)
        {
            score.LootSize = newScore.LootSize;
            score.LootValue = newScore.LootValue;
            score.Service = newScore.Service;
            score.Price = newScore.Price;
        }
    }
}