using System;

namespace Sheriff.Application.DTOs
{
    public class InviteJoinBand
    {
        public Bandit Host { get; set; }
        public Bandit Guest { get; set; }
        public Band Band { get; set; }
    }
}