using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebAppConsumer;
using System.Threading.Tasks;

namespace Messaging
{
    public class RabbitConsume : IRabbitConsume
    {
        readonly IOptions<RabbitSettings> _settings;
        readonly IOptions<MQSettings> _mqSettings;

        readonly IDataEventRepository _repository;

        public RabbitConsume(IOptions<RabbitSettings> settings, IOptions<MQSettings> mqSettings, IDataEventRepository repository)
        {
            _settings = settings;
            _mqSettings = mqSettings;
            _repository = repository;
        }
       
        public async Task ConsumeMessage()
        {
            try
            {
                //var UserName = Environment.GetEnvironmentVariable("ONLINE_STORE_MQ_USERNAME");
                //var onlineStoreMqPassword = Environment.GetEnvironmentVariable("ONLINE_STORE_MQ_PASSWORD");
                //var onlineStoreMqServer = Environment.GetEnvironmentVariable("ONLINE_STORE_MQ_SERVER");

                var factory = new ConnectionFactory()
                {
                    HostName = _settings.Value._hostName,
                    UserName = _settings.Value._userName,
                    Password = _settings.Value._password
                };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: _mqSettings.Value.ExchangeName, type: "fanout", durable: true);

                        channel.QueueDeclare(queue: _mqSettings.Value.QueueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                        channel.QueueBind(queue: _mqSettings.Value.QueueName, exchange: _mqSettings.Value.ExchangeName, routingKey: "");


                        var consumer = new EventingBasicConsumer(channel);

                        BasicGetResult result = channel.BasicGet(_mqSettings.Value.QueueName, true);
                        if (result != null)
                        {
                            string message = Encoding.UTF8.GetString(result.Body);
                            var data = JsonConvert.DeserializeObject<DataEventRecord>(message);
                            _repository.AddDataEventRecord(data);
                        }

                        channel.BasicConsume(queue: _mqSettings.Value.QueueName, autoAck: false, consumer: consumer);
                    }
                }
            }
            catch(Exception)
            {
                
            }
        }
    }
}
