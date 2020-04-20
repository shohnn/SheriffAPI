using System;

namespace Sheriff.Application.DTOs
{
    public class Notification
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}