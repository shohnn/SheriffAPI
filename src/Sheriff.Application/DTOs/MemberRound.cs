using System;

namespace Sheriff.Application.DTOs
{
    public class MemberRound
    {
        public int Id { get; set; }
        public string Member { get; set; }
        public Score Scoring { get; set; }
    }
}