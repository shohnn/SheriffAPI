using System;

namespace Sheriff.Application.DTOs
{
    public class CreateBand
    {
        public string Name { get; set; }
        public Bandit Boss { get; set; }
    }
}