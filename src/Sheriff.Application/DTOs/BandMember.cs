using System;

namespace Sheriff.Application.DTOs
{
    public class BandMember
    {
        public int Id { get; set; }
        public string Band { get; set; }
        public Score Scoring { get; set; }
    }
}