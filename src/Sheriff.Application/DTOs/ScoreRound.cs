using System;

namespace Sheriff.Application.DTOs
{
    public class ScoreRound
    {
        public Round Round { get; set; }
        public Bandit Member { get; set; }
        public Score Score { get; set; }
    }
}