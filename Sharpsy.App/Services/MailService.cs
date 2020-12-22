using Microsoft.AspNetCore.Components;
using MimeKit;
using Sharpsy.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sharpsy.App.Services
{
    public class MailService
    {
        private readonly NavigationManager _navigationManager;
        public MailService()
        {
            //_navigationManager = navigationManager;
        }
        public async Task SendRoomInvitation(RoomInvitationModel roomInvitationModel)
        {

            //var url = _navigationManager.BaseUri + "/invitation/" + roomInvitationModel.InvitationGUID;
            MimeMessage message = new MimeMessage();
            /*
            message.From.Add(new MailboxAddress("Sharpsy", "ganrotwilliam@gmail.com"));
            message.To.Add(new MailboxAddress(roomInvitationModel.ReciverEmail));
            message.Subject = "You have been invited to a Sharpsy room";
            message.Body = new TextPart("plain")
            {
                Text = "this is the invitation project, click here to accept: " + confirmationLink
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("easyscrumhelper@gmail.com", "Admin_123");
                client.Send(message);
                client.Disconnect(true);
            }
            */
        }
    }
}
