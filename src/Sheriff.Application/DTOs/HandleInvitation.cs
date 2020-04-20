using System;

namespace Sheriff.Application.DTOs
{
    public class HandleInvitation
    {
        public bool Accept { get; set; }
        public Invitation Invitation { get; set; }
    }
}