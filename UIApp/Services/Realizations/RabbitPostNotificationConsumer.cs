using Data.DTOs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UIApp.Configuration;
using UIApp.Services.Interfaces;
using UIApp.SignalR.Hubs;

namespace UIApp.Services.Realizations
{
    public class RabbitPostNotificationConsumer : BackgroundService
    {
        private readonly IOptions<RabbitMQConfiguration> _rabbitConfig;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        private readonly IHubContext<NewActivePostNotificationHub> _hubContext;
        private readonly IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitPostNotificationConsumer> _logger;
        private readonly IWatchingPostCacheRepository _cache;
        public RabbitPostNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewActivePostNotificationHub> hub,
            IOptions<QueueNamesConfiguration> queueNames,
            ILogger<RabbitPostNotificationConsumer> logger,
            IWatchingPostCacheRepository cache)
        {
            _logger = logger;

            _cache = cache;

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

        protected override Task ExecuteAsync(CancellationToken cancToken)
        {
            if (!cancToken.IsCancellationRequested)
            {
                try
                {
                    _channel.QueueDeclare(queue: _queueNames.Value.UpdatedPostEventQueue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    var updatedPostConsumer = new AsyncEventingBasicConsumer(_channel);

                    updatedPostConsumer.Received += async (model, ea) =>
                    {
                        var data = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(data);

                        _logger.LogInformation(" --- Rabbit updated post notification consumer consumed a message. --- ");

                        var postDto = JsonConvert.DeserializeObject<PostDto>(message);

                        var usersWatchingPost = await _cache.GetPostWatchersUserIdsByPostId(postDto.Id!.Value.ToString());


                        foreach (var watcher in usersWatchingPost)
                        {
                            await _hubContext.Clients
                                .User(watcher)
                                .SendAsync("ReceivePost", watcher, message);
                        }
                    };                        

                    _channel.BasicConsume(
                        queue: _queueNames.Value.UpdatedPostEventQueue,
                        autoAck: true, consumer: updatedPostConsumer);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                }
            }

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
