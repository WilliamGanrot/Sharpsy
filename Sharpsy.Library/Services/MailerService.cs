using Microsoft.Extensions.Configuration;
using MimeKit;
using Sharpsy.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sharpsy.Library.Services
{
    public class MailerService
    {
        private readonly SmtpSettings _smtpSettings;
        public MailerService(IConfiguration configuration)
        {
            _smtpSettings = configuration.GetSection("Smtp").Get<SmtpSettings>();

        }

        public async Task SendRoomInvitation(RoomInvitationModel roomInvitationModel)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("Sharpsy", _smtpSettings.From));
            message.To.Add(new MailboxAddress(address: roomInvitationModel.ReciverEmail));
            message.Subject = "You have been invited to a Sharpsy room";
            message.Body = new TextPart("plain")
            {
                Text = "this is the invitation project, click here to accept: " + roomInvitationModel.InvitationUrl
            }; 

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_smtpSettings.Host, _smtpSettings.Port, true);
                client.Authenticate(_smtpSettings.From, _smtpSettings.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
