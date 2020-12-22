using Dapper;
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
    public class RoomStore : IRoomStore
    {
        private readonly string _connectionString;
        public RoomStore(string ConnectionString)
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
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<int> UpdateDocument(RoomModel room)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new { room.Title };
                return await connection.ExecuteAsync("dbo.spUpdateRoom", parameters);
            }
        }

        public async Task<int> DeleteRoom(int id)
        {
            using (var connections = new SqlConnection(_connectionString))
            {
                var parameters = new { RoomId = id };
                return await connections.ExecuteAsync("dbo.spDeleteRoom", parameters);

            }
        }

        public async Task<RoomModel> FindRoomById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<RoomModel>(
                    Queries.GetRoomById,
                    new { 
                        RoomId = id
                    });
                
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
    }
}
