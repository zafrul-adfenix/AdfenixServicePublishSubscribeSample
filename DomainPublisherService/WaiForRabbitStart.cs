using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace DomainPublisherService
{
    class WaiForRabbitStart
    {
        public static async Task WaitForRabbitToStart()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Password = "guest",
                UserName = "guest"
            };
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    using (factory.CreateConnection())
                    {
                    }
                    return;
                }
                catch (BrokerUnreachableException)
                {
                }
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }

    }
}
