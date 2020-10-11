using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SampleFunctionTwo
{
    public static class ServiceBusSubscriberOne
    {
        [FunctionName("ServiceBusSubscriberOne")]
        public static void Run([ServiceBusTrigger("graph-subsc", "service-bus-subsc-one", Connection = "ServiceConnection")]Message mySbMsg, ILogger log)
        {
            var d = mySbMsg.UserProperties["Data"];
            log.LogInformation($"{d}");
        }
    }
}
