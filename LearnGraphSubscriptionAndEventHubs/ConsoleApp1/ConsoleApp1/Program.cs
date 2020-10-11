using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var topicName = "graph-subsc";
            var subscriptionName = "service-bus-subsc-console";
            string entityPath = EntityNameHelper.FormatSubscriptionPath(topicName, subscriptionName);

            string serviceConnection = ConfigurationManager.AppSettings.Get("ServiceConnection");

            var receiver = new MessageReceiver(serviceConnection, entityPath, ReceiveMode.PeekLock, RetryPolicy.Default, 100);

            while (true)
            {
                try
                {
                    IList<Message> messages = await receiver.ReceiveAsync(10, TimeSpan.FromSeconds(2));
                    if (messages == null)
                    {
                        continue;
                    }
                    if (messages.Any())
                    {
                        foreach (var message in messages)
                        {
                            lock (Console.Out)
                            {
                                var messageProp = message.UserProperties;
                                var d = messageProp["Data"];
                                Console.WriteLine(d);
                            }
                            await receiver.CompleteAsync(message.SystemProperties.LockToken);
                        }
                    }
                    else
                    {
                        Console.WriteLine("BREAK");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            await receiver.CloseAsync();
            Console.WriteLine("EXIT");
        }
    }
}
