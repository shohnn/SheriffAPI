using System;

namespace Sheriff.Application.DTOs
{
    public class BandDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MemberBand[] Members { get; set; }
        public Round[] Rounds { get; set; }
    }
}