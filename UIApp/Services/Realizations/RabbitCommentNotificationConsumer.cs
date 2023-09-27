using Data.DTOs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIApp.Configuration;
using UIApp.Services.Interfaces;
using UIApp.SignalR.Hubs;

namespace UIApp.Services.Realizations
{
    public class RabbitCommentNotificationConsumer : IRabbitQueueConsumer
    {
        private readonly IOptions<RabbitMQConfiguration> _rabbitConfig;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        private readonly IHubContext<NewPassiveCommentNotificationHub> _hubContext;
        private readonly IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitCommentNotificationConsumer> _logger;
        public RabbitCommentNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewPassiveCommentNotificationHub> hub,
            ILogger<RabbitCommentNotificationConsumer> logger,
            IOptions<QueueNamesConfiguration> queueNames)
        {
            _logger = logger;

            _rabbitConfig = rabbitConfig;

            _hubContext = hub;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitConfig.Value.Host,
                Port = (int)_rabbitConfig.Value.Port!,
                Password = _rabbitConfig.Value.Password,
                UserName = _rabbitConfig.Value.Username,
            };

            _connection = _connectionFactory.CreateConnection();

            _channel = _connection.CreateModel();

            _queueNames = queueNames;
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            _logger.LogInformation(" --- Rabbit comment notification consumer has been successfully disposed. --- ");
        }

        public async Task Consume(CancellationToken cancToken)
        {
            _logger.LogInformation(" --- Rabbit comment notification consumer started to consume --- ");
            
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitConfig.Value.Host,
                Port = (int)_rabbitConfig.Value.Port!,
                Password = _rabbitConfig.Value.Password,
                UserName = _rabbitConfig.Value.Username,
            };

            using var connection = connectionFactory.CreateConnection();

            using var channel = connection.CreateModel();

            var queueDeclare = channel.QueueDeclare(queue: _queueNames.Value.CreatedCommentEventQueue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            BasicGetResult? result;

            do
            {
                result = channel.BasicGet(_queueNames.Value.CreatedCommentEventQueue, false);
                if (result != null)
                {
                    var data = result.Body.ToArray();
                    string message = Encoding.UTF8.GetString(data);

                    var commentDto = JsonConvert.DeserializeObject<CommentDto>(message);

                    await _hubContext.Clients
                        .Users(commentDto.Post!.OwnerId!.Value.ToString())
                        .SendAsync("ReceiveComment", commentDto.Post!.OwnerId!.Value.ToString(), message);

                    channel.BasicAck(result.DeliveryTag, false);
                }
            }
            while (result != null && !cancToken.IsCancellationRequested);
            _logger.LogInformation(" --- Rabbit comment notification consumer stopped consuming --- ");
        }
    }
}
