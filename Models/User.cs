using System;
using System.Collections.Generic;

namespace SignalRChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public int Age { get; set; }

        public int Sex { get; set; }

        public DateTime LastVisit { get; set; }

        public string Password { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual List<User> Friends { get; set; } 
    }
}