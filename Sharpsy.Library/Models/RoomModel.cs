using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpsy.Library.Models
{
    public class RoomModel
    {
        public int RoomId { get; set; }
        public string Title { get; set; }
        public int CreatorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }


        public ApplicationUser Creator { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
