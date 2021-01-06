using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Sharpsy.DataAccess.Stores;

namespace Sharpsy.Procedures
{

    //TODO - Implement dependency Injection (or some kind of IoC)
    public static class CheckForExpiredRoomInvitations
    {
        private static readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sharpsy.sql;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static readonly IStorage _roomStore = new Storage(_connectionString);

        [FunctionName("CheckForExpiredRoomInvitations")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            var start = DateTime.Now;
            await _roomStore.LookForExpieringRoomInvitations();
            log.Info($"CheckForExpiredRoomInvitations function executed at: {start} and completed at{DateTime.Now}");
        }
    }
}
