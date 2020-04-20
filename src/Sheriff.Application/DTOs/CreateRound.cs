using System;

namespace Sheriff.Application.DTOs
{
    public class CreateRound
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime DateTime { get; set; }

        public Band Band { get; set; }

        public Bandit Sheriff { get; set; }

        public Bandit[] Members { get; set; }
    }
}