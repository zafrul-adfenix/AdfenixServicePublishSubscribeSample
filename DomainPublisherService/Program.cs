using System;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace DomainPublisherService
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task  MainAsync()
        {
            RabbitSettings service = new RabbitSettings();

            // The RabbitMQ container starts before endpoints but it may
            // take several seconds for the broker to become reachable.
            await WaiForRabbitStart.WaitForRabbitToStart()
                .ConfigureAwait(false);

            IConnection connection = service.GetRabbitMqConnection();
            IModel model = connection.CreateModel();
            model.QueueDeclare(RabbitSettings.SerialisationQueueName, true, false, false, null);

            SendSerializedObject(model);
        }

        private static void SetupSerialisationMessageQueue(IModel model)
        {
            
        }
        private static void SendSerializedObject(IModel model)
        {
            MessageData data = new MessageData();
            data.Name = "Message Publisher";
            data.Description = "Message Description";            
            IBasicProperties basicProperties = model.CreateBasicProperties();
            basicProperties.Persistent = true;

            String jsonified = JsonConvert.SerializeObject(data);
            byte[] customerBuffer = Encoding.UTF8.GetBytes(jsonified);
            model.BasicPublish("", RabbitSettings.SerialisationQueueName, basicProperties, customerBuffer);            
        }
    }
}
