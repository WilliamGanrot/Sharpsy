using Sharpsy.Library.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharpsy.Library.Models
{
    public class RoomInvitationModel
    {
        public int RoomInvitationId { get; set; }
        public int SenderUserId { get; set; }
        public int RoomId { get; set; }
        public string ReciverEmail { get; set; }
        public string InvitationGUID { get; set; }
        public DateTime Created { get; set; }
        public string InvitationUrl { get; set; }
        public RoomInvitationStatus Status { get; set; }

        public ApplicationUser Sender { get; set; }
        public ApplicationUser InvitedUser { get; set; }
        public RoomModel Room { get; set; }
    }
}
