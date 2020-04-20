using System;

namespace Sheriff.Application.DTOs
{
    public class BanditDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Score Scoring { get; set; }
        public BandMember[] Bands { get; set; }
    }
}