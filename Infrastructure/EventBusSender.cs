using Application.Common.Interfaces;
using Domain.Common;
using Domain.Events;
using Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EventBusSender : IEventBusSender
    {
        private readonly IOptions<RabbitMQConfiguration> RabbitConfig;
        private readonly IModel channel;
        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly ILogger _logger;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        
        public EventBusSender(IOptions<QueueNamesConfiguration> queueNames, IOptions<RabbitMQConfiguration> rabbitConfig, ILogger<EventBusSender> logger) 
        {
            RabbitConfig = rabbitConfig;
            connectionFactory = new ConnectionFactory()
            {
                HostName = rabbitConfig.Value.Host,
                Port = (int)rabbitConfig.Value.Port!,
                UserName = rabbitConfig.Value.Username,
                Password = rabbitConfig.Value.Password,
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            _logger = logger;
            _queueNames = queueNames;
        }

        public Task Send<T>(T _event) where T : BaseEvent
        {
            string? message = string.Empty;
            string? queue = string.Empty;
            switch (_event)
            {
                case CreatedReactionEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.CreatedReactionEventQueue;
                    break;
                case CreatedMessageEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.CreatedMessageEventQueue;
                    break;
                case CreatedFriendshipEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.CreatedFriendshipEventQueue;
                    break;
                case CreatedFriendrequestEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.CreatedFriendrequestEventQueue;
                    break;
                case CreatedCommentEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.CreatedCommentEventQueue;
                    break;
                case UpdatedPostEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.UpdatedPostEventQueue;
                    break;
                case UpdatedMessageEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.UpdatedMessageEventQueue;
                    break;
                case RemovedReactionEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.RemovedReactionEventQueue;
                    break;
                case RemovedMessageEvent __event:
                    message = JsonConvert.SerializeObject(__event.Event, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    queue = _queueNames.Value.RemovedMessageEventQueue;
                    break;
            };

            channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: queue,
                body: body
                );

            _logger.LogInformation($"Message \"{message}\" was sent into the \"{queue}\" queue");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            channel?.Dispose();
            connection?.Dispose();
            _logger.LogInformation($"Event bus sender has been successfully disposed.");
        }
    }
}
