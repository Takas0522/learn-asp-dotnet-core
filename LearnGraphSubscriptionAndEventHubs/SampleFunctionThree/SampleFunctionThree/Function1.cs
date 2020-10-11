using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SampleFunctionThree
{
    public static class ServiceBusSubscriberTwo
    {
        [FunctionName("ServiceBusSubscriberTwo")]
        public static void Run([ServiceBusTrigger("graph-subsc", "service-bus-subsc-two", Connection = "ServiceConnection")] Message mySbMsg, ILogger log)
        {
            var d = mySbMsg.UserProperties["Data"];
            log.LogInformation($"ServiceBusSubscriber 2 Runnning");
            log.LogInformation($"{d}");
        }
    }
}
