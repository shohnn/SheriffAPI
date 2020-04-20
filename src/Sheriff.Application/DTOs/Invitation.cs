using System;

namespace Sheriff.Application.DTOs
{
    public class Invitation
    {
        public int Id { get; set; }
        public Bandit Guest { get; set; }
        public Band Band { get; set; }
    }
}