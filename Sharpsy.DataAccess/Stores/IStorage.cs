﻿using Sharpsy.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sharpsy.DataAccess.Stores
{
    public interface IStorage
    {
        Task CreateRoom(RoomModel room);
        Task<RoomModel> FindRoomById(int id);
        Task<IEnumerable<RoomModel>> GetRoomsByUserId(int id);
        Task<bool> InsertInvitation(RoomInvitationModel roomInvitationModel);
        Task<RoomInvitationModel> GetRoomInvitation(string InvidationGuid);
        Task AccpetRoomInvitation(RoomInvitationModel invitation);
        Task DeclineRoomInvitation(RoomInvitationModel invitation);
        Task<int> LookForExpieringRoomInvitations();
        Task<bool> IsUserInRoom(int userId, int roomId);
        Task<int> InsertMessage(Message message);
        Task<IEnumerable<Message>> GetMessagePageInRoom(int roomId, int page);
    }
}