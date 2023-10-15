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
    public class RabbitReactionNotificationConsumer : BackgroundService
    {
        private readonly IOptions<RabbitMQConfiguration> _rabbitConfig;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        private readonly IHubContext<NewPassiveReactionNotificationHub> _passiveReactionHubContext;
        private readonly IHubContext<NewActiveReactionNotificationHub> _activeReactionHubContext;
        private readonly IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitReactionNotificationConsumer> _logger;
        private readonly IWatchingPostCacheRepository _cache;
        private readonly Semaphore _semaphore;
        public RabbitReactionNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewActiveReactionNotificationHub> activeHub,
            IHubContext<NewPassiveReactionNotificationHub> passiveHub,
            ILogger<RabbitReactionNotificationConsumer> logger,
            IOptions<QueueNamesConfiguration> queueNames,
            IWatchingPostCacheRepository cache)
        {
            _semaphore = new Semaphore(10,10);

            _logger = logger;

            _cache = cache;

            _rabbitConfig = rabbitConfig;

            _passiveReactionHubContext = passiveHub;

            _activeReactionHubContext = activeHub;

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
        public override void Dispose()
        {
            base.Dispose();
            _channel?.Dispose();
            _connection?.Dispose();
            _logger.LogInformation(" --- Rabbit reaction notification consumer has been successfully disposed. --- ");
        }

        protected override async Task ExecuteAsync(CancellationToken cancToken)
        {
            while(!cancToken.IsCancellationRequested)
            {
                await Consume(cancToken);
                await Task.Delay(1);
            }
        }

        public async Task Consume(CancellationToken cancToken = default)
        {
            _semaphore.WaitOne();

            _logger.LogInformation(" --- Rabbit reaction notification consumer started to consume. --- ");
            try
            {

                _channel.QueueDeclare(queue: _queueNames.Value.CreatedReactionEventQueue,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                _channel.QueueDeclare(queue: _queueNames.Value.RemovedReactionEventQueue,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                BasicGetResult? createdReactionResult;
                BasicGetResult? removedReactionResult;

                do
                {
                    createdReactionResult = _channel.BasicGet(_queueNames.Value.CreatedReactionEventQueue, false);
                    removedReactionResult = _channel.BasicGet(_queueNames.Value.RemovedReactionEventQueue, false);

                    if (createdReactionResult != null)
                    {
                        var data = createdReactionResult.Body.ToArray();
                        string message = Encoding.UTF8.GetString(data);

                        var reactionDto = JsonConvert.DeserializeObject<ReactionDto>(message);

                        var postWatchers = await _cache.GetPostWatchersUserIdsByPostId(reactionDto.PostId!.Value.ToString());

                        bool isOwnerWatching = false;

                        foreach (var watcher in postWatchers)
                        {
                            if (watcher == reactionDto.Post!.OwnerId!.Value.ToString())
                                isOwnerWatching = true;
                            await _activeReactionHubContext.Clients
                                .User(watcher)
                                .SendAsync("ReceiveReaction", watcher, message);
                        }

                        if (!isOwnerWatching)
                        {
                            await _passiveReactionHubContext.Clients
                                .User(reactionDto.Post!.OwnerId!.Value.ToString())
                                .SendAsync("ReceiveReaction", reactionDto.Post!.OwnerId!.Value.ToString(), message);
                        }

                        _channel.BasicAck(createdReactionResult.DeliveryTag, false);
                    }

                    if (removedReactionResult != null)
                    {
                        var data = removedReactionResult.Body.ToArray();

                        string message = Encoding.UTF8.GetString(data);

                        var reactionDto = JsonConvert.DeserializeObject<ReactionDto>(message);

                        var postWatchers = await _cache.GetPostWatchersUserIdsByPostId(reactionDto.PostId!.Value.ToString());

                        foreach (var watcher in postWatchers)
                        {
                            await _activeReactionHubContext.Clients
                            .Users(watcher)
                            .SendAsync("RemoveReaction", watcher, message);
                        }

                        _channel.BasicAck(removedReactionResult.DeliveryTag, false);
                    }
                }
                while ((createdReactionResult != null || removedReactionResult != null) && !cancToken.IsCancellationRequested);
            }
            catch(Exception ex) 
            {
                _logger.LogCritical(ex.Message);
            }

            _semaphore.Release();

            _logger.LogInformation(" --- Rabbit reaction consumer stopped consuming. --- ");
        }
    }
}
