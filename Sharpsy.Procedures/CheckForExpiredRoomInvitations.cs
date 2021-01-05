using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Sharpsy.Procedures
{

    //TODO - Implement dependency Injection (or some kind of IoC)
    public static class CheckForExpiredRoomInvitations
    {
        [FunctionName("CheckForExpiredRoomInvitations")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
