using Microsoft.AspNetCore.SignalR;
using Sharpsy.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharpsy.App
{
    public class UserConnection
    {
        public string ConnectionId { get; set; }
        public int UserId { get; set; }

        public UserConnection(string cId, int uId)
        {
            ConnectionId = cId;
            UserId = uId;
        }
    }

    public class ChatHub : Hub
    {
        public const string HubUrl = "/ChatHub";
        private static List<UserConnection> activeUsers = new List<UserConnection>();


        public Task JoinGroup(string group, int userId)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, group);
            activeUsers.Add(new UserConnection(Context.ConnectionId, userId));

            return base.OnConnectedAsync();
        }
            
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            activeUsers.RemoveAll(p => p.ConnectionId == Context.ConnectionId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public Task SendMessageToGroup(string group, SimpleMessage message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public Task UpdateActiveUserList(List<int> l)
        {
            var x = activeUsers.Select(x => x.UserId).ToList().Intersect(l).ToList();
            return Clients.All.SendAsync("ReciveActiveList", x);
        }

    }
}
