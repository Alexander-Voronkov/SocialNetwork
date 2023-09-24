using Data.DTOs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UIApp.Configuration;
using UIApp.Services.Interfaces;
using UIApp.SignalR.Hubs;

namespace UIApp.Services.Realizations
{
    public class RabbitFriendshipNotificationConsumer : IRabbitQueueConsumer
    {
        private readonly IOptions<RabbitMQConfiguration> _rabbitConfig;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        private readonly IHubContext<NewPassiveFriendrequestNotificationHub> _hubContext;
        private readonly IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitFriendshipNotificationConsumer> _logger;
        public RabbitFriendshipNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewPassiveFriendrequestNotificationHub> hub,
            ILogger<RabbitFriendshipNotificationConsumer> logger,
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
            _logger.LogInformation(" --- Rabbit friendship consumer has been successfully disposed. --- ");
        }

        public async Task Consume(CancellationToken cancToken)
        {
            _logger.LogInformation(" --- Rabbit friendship consumer started consuming. --- ");

            var queueDeclare = _channel.QueueDeclare(queue: _queueNames.Value.CreatedFriendshipEventQueue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            BasicGetResult? result;

            do
            {
                result = _channel.BasicGet(_queueNames.Value.CreatedFriendshipEventQueue, false);
                if (result != null)
                {
                    var data = result.Body.ToArray();
                    string message = Encoding.UTF8.GetString(data);

                    var friendshipDto = JsonConvert.DeserializeObject<FriendshipDto>(message);

                    await _hubContext.Clients
                        .Users(friendshipDto!.FirstUserId!.Value.ToString())
                        .SendAsync("ReceiveFriendrequest", friendshipDto!.FirstUserId!.Value.ToString(), friendshipDto);

                    _channel.BasicAck(result.DeliveryTag, false);
                }
            }
            while (result != null && !cancToken.IsCancellationRequested);

            _logger.LogInformation(" --- Rabbit friendship consumer stopped consuming. --- ");
        }
    }
}
