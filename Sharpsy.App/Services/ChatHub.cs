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
        public List<int> RoomIds { get; set; }

        public UserConnection(string cId, int uId, List<int> rIds = null)
        {
            ConnectionId = cId;
            UserId = uId;
            RoomIds = rIds;
        }
    }

    //todo tell to update clients active user list on disconnect
    public class ChatHub : Hub
    {
        public const string HubUrl = "/ChatHub";
        private static List<UserConnection> activeUsers = new List<UserConnection>();


        public Task JoinGroup(string group, int userId, List<int> allUserRooms)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, group);
            activeUsers.Add(new UserConnection(Context.ConnectionId, userId, allUserRooms));
            TellClientTopUppdateActiveList(group);
            return base.OnConnectedAsync();
        }
            
        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var userConnection = activeUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            activeUsers.RemoveAll(p => p.ConnectionId == Context.ConnectionId);
            
            foreach(var r in userConnection.RoomIds)
            {
                TellClientTopUppdateActiveList(r.ToString());
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public Task SendMessageToGroup(string group, SimpleMessage message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public Task TellClientTopUppdateActiveList(string group)
        {
            return Clients.Group(group).SendAsync("TellClientTopUppdateActiveList");
        }

        public Task GetActiveUserList(List<int> allRoomUsers)
        {
            var activeUsersInRoom = activeUsers.Select(x => x.UserId).ToList().Intersect(allRoomUsers).ToList();
            return Clients.All.SendAsync("ReciveActiveList", activeUsersInRoom);
        }


    }
}
