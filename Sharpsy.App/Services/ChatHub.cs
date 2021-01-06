using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharpsy.App
{
    public class ChatHub : Hub
    {
        public const string HubUrl = "/ChatHub";

        public Task JoinGroup(string group) =>
            Groups.AddToGroupAsync(Context.ConnectionId, group);
        
        public Task LeaveGroup(string group) => 
            Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

        public Task SendMessageToGroup(string group, string message) =>
            Clients.Group(group).SendAsync("ReceiveMessage", message);
    }
}
