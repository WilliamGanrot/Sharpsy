using Sharpsy.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sharpsy.DataAccess.Stores
{
    public interface IRoomStore
    {
        Task CreateRoom(RoomModel room);
        Task<int> DeleteRoom(int id);
        Task<RoomModel> FindRoomById(int id);
        Task<IEnumerable<RoomModel>> GetRoomsByUserId(int id);
        Task<int> UpdateDocument(RoomModel room);
        Task<bool> InsertInvitation(RoomInvitationModel roomInvitationModel);
        Task<RoomInvitationModel> GetRoomInvitation(string InvidationGuid);
    }
}