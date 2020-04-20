using System;

namespace Sheriff.Application.DTOs
{
    public class RequestJoinBand
    {
        public Bandit Guest { get; set; }
        public Band Band { get; set; }
    }
}