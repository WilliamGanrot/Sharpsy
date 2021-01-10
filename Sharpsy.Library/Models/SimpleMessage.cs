using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpsy.Library.Models
{
    public class SimpleMessage
    {
        public string SenderEmail { get; set; }
        public DateTime Sent { get; set; }
        public string Text { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }

    }

}
