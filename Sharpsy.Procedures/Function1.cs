using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Sharpsy.DataAccess.Stores;

namespace Sharpsy.Procedures
{
    public class Function1
    {
        private readonly IRoomStore _roomStore;
        public Function1()
        {
            _roomStore = new RoomStore("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sharpsy.sql;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ExecutionContext context)
         {
            _roomStore.LookForExpieringRoomInvitations();
            //log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}