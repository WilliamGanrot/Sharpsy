using System;
using System.Collections.Generic;
using System.Linq;
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


        public List<SimpleMessage> GetSimpleMessages() =>
            Messages.Select(m => new SimpleMessage
                {
                    Text = m.Text,
                    SenderEmail = m.User.Email,
                    Sent = m.Sent,
                    UserId = m.UserId
                }).ToList();

        public SimpleMessage NotificationMessage { get; set; }

    }
}
    