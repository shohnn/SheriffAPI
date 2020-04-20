using System;

namespace Sheriff.Application.DTOs
{
    public class MemberBand
    {
        public int Id { get; set; }
        public string Member { get; set; }
        public Score Scoring { get; set; }
    }
}