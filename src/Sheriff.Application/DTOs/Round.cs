using System;

namespace Sheriff.Application.DTOs
{
    public class Round
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
    }
}