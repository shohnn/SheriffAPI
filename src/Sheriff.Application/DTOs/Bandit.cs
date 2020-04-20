using System;

namespace Sheriff.Application.DTOs
{
    public class Bandit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Score Scoring { get; set; }
    }
}