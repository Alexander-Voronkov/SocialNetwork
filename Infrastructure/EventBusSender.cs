using Application.Common.Interfaces;
using Domain.Common;
using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared;
using System.Text;

namespace Infrastructure
{
    public class EventBusSender : IEventBusSender
    {
        private readonly IOptions<RabbitMQConfiguration> RabbitConfig;
        private readonly IModel channel;
        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection connection;
        private readonly ILogger _logger;
         
        public EventBusSender(IOptions<RabbitMQConfiguration> rabbitConfig, ILogger<EventBusSender> logger) 
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
        }

        public Task Send<T>(T _event) where T : BaseEvent
        {
            string? message = string.Empty;
            string? queue = string.Empty;

            switch (_event)
            {
                case CreatedPostEvent createdPostEvent: 
                    message = createdPostEvent.Post.Id.ToString();
                    queue = "social-network-created-post-event"; 
                    break;
                case CreatedReactionEvent createdReactionEvent:
                    message = createdReactionEvent.Reaction.Id.ToString(); 
                    queue = "social-network-created-reaction-event";
                    break;
                case CreatedUserEvent createdUserEvent: 
                    message = createdUserEvent.User.Id.ToString(); 
                    queue = "social-network-created-user-event"; 
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

        public Task CloseConnection()
        {
            channel.Dispose();
            connection.Dispose();
            return Task.CompletedTask;
        }
    }
}
