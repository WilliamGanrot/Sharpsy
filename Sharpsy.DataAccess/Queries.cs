using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharpsy.DataAccess
{
    public static class Queries
    {
        static Queries(){
            var dir = AppDomain.CurrentDomain.BaseDirectory;

            GetRoomsByUserId = File.ReadAllText(Path.Combine(dir, "Queries/get_rooms_by_userId.sql"));
            InsertRoom = File.ReadAllText(Path.Combine(dir, "Queries/room_insert.sql"));
            InsertApplicationUserRoom = File.ReadAllText(Path.Combine(dir, "Queries/applicationuser_room_insert.sql"));
            GetRoomById = File.ReadAllText(Path.Combine(dir, "Queries/room_get_by_id.sql"));
            InserInvitation = File.ReadAllText(Path.Combine(dir, "Queries/invitation_insert.sql"));
            GetFullInvitation = File.ReadAllText(Path.Combine(dir, "Queries/invitation_get_by_id_full.sql"));
            UpdateRoomInvitationStatus = File.ReadAllText(Path.Combine(dir, "Queries/roominvitation_update_status.sql"));
            
        }
        

        public static string GetRoomsByUserId { get; set; }
        public static string InsertRoom { get; set; }
        public static string InsertApplicationUserRoom { get; set; }
        public static string GetRoomById { get; set; }
        public static string InserInvitation { get; set; }
        public static string GetFullInvitation { get; set; }
        public static string UpdateRoomInvitationStatus { get; set; }
    }
}
