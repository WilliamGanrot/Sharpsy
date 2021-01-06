using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpsy.Library.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime Sent { get; set; }
        public string Text { get; set; }


        public RoomModel Room { get; set; }
        public ApplicationUser User { get; set; }
    }
}
