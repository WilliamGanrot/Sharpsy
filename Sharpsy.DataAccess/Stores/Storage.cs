using Dapper;
using Sharpsy.Library.Enums;
using Sharpsy.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpsy.DataAccess.Stores
{
    public class Storage : IStorage
    {
        private readonly string _connectionString;
        public Storage(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public async Task CreateRoom(RoomModel room)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var roomId = (int)await connection.ExecuteScalarAsync(
                            Queries.InsertRoom,
                            new { 
                                CreatorId = room.CreatorId,
                                Title = room.Title, 
                            },
                            transaction);

                        await connection.ExecuteScalarAsync(
                            Queries.InsertApplicationUserRoom,
                            new { 
                                RoomId = roomId, 
                                UserId = room.CreatorId 
                            },
                            transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task<RoomModel> FindRoomById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<RoomModel>(
                    Queries.GetRoomById,
                    new { RoomId = id });
            }
        }
        public async Task<IEnumerable<RoomModel>> GetRoomsByUserId(int id)
        {
            var lookup = new Dictionary<int, RoomModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new { UserId = id };

                await connection.QueryAsync<RoomModel, ApplicationUserRoom, RoomModel>(
                    Queries.GetRoomsByUserId,
                    (r, ar) =>
                    {
                        RoomModel room;
                        if (!lookup.TryGetValue(r.RoomId, out room))
                            lookup.Add(r.RoomId, r);

                        return room;
                    },
                    parameters,
                    splitOn: "RoomId,ApplicationUserRoomId");

                return lookup.Values.ToList();
            }
        }
        
        public async Task<bool> InsertInvitation(RoomInvitationModel roomInvitationModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var affected = await connection.ExecuteAsync(
                    Queries.InserInvitation,
                    new
                    {
                        ReciverEmail = roomInvitationModel.ReciverEmail,
                        SenderUserId = roomInvitationModel.SenderUserId,
                        RoomId = roomInvitationModel.RoomId,
                        InvitationGUID = roomInvitationModel.InvitationGUID.ToString()
                    });

                return affected == 1;
            }
        }

        public async Task<RoomInvitationModel> GetRoomInvitation(string InvitationGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new { InvitationGuid = InvitationGuid };

                var invitation = await connection.QueryAsync<RoomInvitationModel, RoomModel, ApplicationUser, RoomInvitationModel>(
                    Queries.GetFullInvitation, 
                    (roomInvitation, room, user) => {
                        roomInvitation.Room = room;
                        roomInvitation.Sender = user;
                        return roomInvitation;
                    },
                    parameters,
                    splitOn: "RoomInvitationId, RoomId, Id");

                return invitation.FirstOrDefault();
            }
        }

        public async Task AccpetRoomInvitation(RoomInvitationModel invitation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(
                            Queries.UpdateRoomInvitationStatus,
                            new
                            {
                                Status = (int)RoomInvitationStatus.Accepted,
                                InvitationId = invitation.RoomInvitationId
                            },
                            transaction);


                        await connection.ExecuteScalarAsync(
                            Queries.InsertApplicationUserRoom,
                            new
                            {
                                RoomId = invitation.RoomId,
                                UserId = invitation.InvitedUser.Id
                            },
                            transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task DeclineRoomInvitation(RoomInvitationModel invitation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new { Status = (int)RoomInvitationStatus.Declined, InvitationId = invitation.RoomInvitationId };
                await connection.ExecuteAsync(Queries.UpdateRoomInvitationStatus, parameters);
            }
        }

        public async Task<int> LookForExpieringRoomInvitations()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(Queries.LookForExpireringRoomInvitations);
            }
        }

        public async Task<bool> IsUserInRoom(int userId, int roomId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var res =  await connection.QueryFirstOrDefaultAsync<ApplicationUserRoom>(
                    Queries.IsUserInRoom,
                    new { 
                        RoomId = roomId,
                        UserId = userId
                    });

                return res != null;
            }
        }

        public async Task<int> InsertMessage(Message message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<int>(
                    Queries.InsertMessage,
                    new
                    {
                        UserId = message.UserId,
                        Text = message.Text,
                        RoomId = message.RoomId
                    });
            }
        }
        public async Task<IEnumerable<Message>> GetMessagePageInRoom(int roomId, int page)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var lookup = new Dictionary<int, Message>();
                await connection.QueryAsync<Message, ApplicationUser, RoomModel, Message>(
                    Queries.GetMessagePageInRoom,
                    (m, u, r) =>
                    {
                        Message message;
                        if (!lookup.TryGetValue(m.MessageId, out message))
                            lookup.Add(m.MessageId, message = m);

                        message.User = u;
                        message.Room = r;

                        return message;
                    },
                    new
                    {
                        RoomId = roomId,
                        Offset = (page * 100) + 100
                    },
                    splitOn: "MessageId,Id,RoomId");

                return lookup.Values.ToList();
            }
        }
    }
}
