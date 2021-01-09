using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharpsy.Library.Models;

namespace Sharpsy.App
{
    public class ChatHub : Hub
    {
        public const string HubUrl = "/ChatHub";

        public Task JoinGroup(string group)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, group);
            return base.OnConnectedAsync();
        }
            
        
        public Task LeaveGroup(string group) => 
            Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

        public Task SendMessageToGroup(string group, SimpleMessage message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }
            
    }
}
