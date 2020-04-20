using System;

namespace Sheriff.Application.DTOs
{
    public class RoundDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public MemberRound Sheriff { get; set; }
        public MemberRound[] Members { get; set; }
    }
}