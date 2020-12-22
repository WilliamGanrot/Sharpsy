using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpsy.Library.Models
{
    public class RoomInvitationModel
    {
        public int Id { get; set; }
        public int SenderUserId { get; set; }
        public int RoomId { get; set; }
        public string ReciverEmail { get; set; }
        public Guid InvitationGUID { get; set; }
        public DateTime Created { get; set; }
        public string InvitationUrl { get; set; }
    }
}
