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
    public class RabbitPostNotificationConsumer : BackgroundService
    {
        private readonly IOptions<RabbitMQConfiguration> _rabbitConfig;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        private readonly IHubContext<NewActivePostNotificationHub> _hubContext;
        private readonly IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitPostNotificationConsumer> _logger;
        private readonly Semaphore _semaphore;
        private readonly IWatchingPostCacheRepository _cache;
        public RabbitPostNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewActivePostNotificationHub> hub,
            IOptions<QueueNamesConfiguration> queueNames,
            ILogger<RabbitPostNotificationConsumer> logger,
            IWatchingPostCacheRepository cache)
        {
            _semaphore = new Semaphore(10, 10);

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

        protected async override Task ExecuteAsync(CancellationToken cancToken)
        {
            while (!cancToken.IsCancellationRequested)
            {
                await Consume(cancToken);
                await Task.Delay(1);
            }
        }

        public async Task Consume(CancellationToken cancToken)
        {
            _semaphore.WaitOne();

            _logger.LogInformation(" --- Rabbit updated post notification consumer started to consume. --- ");

            try
            {
                _channel.QueueDeclare(queue: _queueNames.Value.UpdatedPostEventQueue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                BasicGetResult? updatedPostResult;

                do
                {
                    updatedPostResult = _channel.BasicGet(_queueNames.Value.UpdatedPostEventQueue, false);
                    if (updatedPostResult != null)
                    {
                        var data = updatedPostResult.Body.ToArray();
                        string message = Encoding.UTF8.GetString(data);

                        _logger.LogInformation(" --- Rabbit updated post notification consumer consumed a message. --- ");

                        var postDto = JsonConvert.DeserializeObject<PostDto>(message);

                        var usersWatchingPost = await _cache.GetPostWatchersByPostId(postDto.Id!.Value.ToString());

                        foreach(var watcher in usersWatchingPost)
                        {
                            await _hubContext.Clients
                            .User(watcher)
                            .SendAsync("ReceivePost", watcher, message);
                        }

                        _channel.BasicAck(updatedPostResult.DeliveryTag, false);
                    }
                }
                while (updatedPostResult != null && !cancToken.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            _semaphore.Release();

            _logger.LogInformation(" --- Rabbit message consumer stopped consuming. --- ");
        }

        public override void Dispose()
        {
            base.Dispose();
            _channel?.Dispose();
            _connection?.Dispose();
            _logger.LogInformation(" --- Rabbit message consumer has been successfully disposed. --- ");
        }
    }
}
