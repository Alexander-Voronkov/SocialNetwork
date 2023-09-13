using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;
using UIApp.Services.Interfaces;
using static Hangfire.Storage.JobStorageFeatures;
using Shared;

namespace UIApp.Services.Realizations
{
    public class RabbitQueueConsumer : IRabbitQueueConsumer
    {
        private readonly IOptions<RabbitMQConfiguration> RabbitConfig;
        private readonly ILogger _logger;
        public RabbitQueueConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig, 
            ILogger<RabbitQueueConsumer> logger)
        {
            RabbitConfig = rabbitConfig;
            _logger = logger;
        }

        public void Consume(string queueName, IRabbitQueueMessageProcessor whatToDo)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = RabbitConfig.Value.Host,
                Port = (int)RabbitConfig.Value.Port!,
                Password = RabbitConfig.Value.Password,
                UserName = RabbitConfig.Value.Username,
            };

            using var connection = connectionFactory.CreateConnection();

            using var channel = connection.CreateModel();

            var queueDeclare = channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            BasicGetResult? result;

            do
            {
                result = channel.BasicGet(queueName, false);
                if (result != null)
                {
                    var data = result.Body.ToArray();
                    string message = Encoding.UTF8.GetString(data);

                    whatToDo.Process(message);

                    channel.BasicAck(result.DeliveryTag, false);
                }
            }
            while (result != null);
        }
    }
}
