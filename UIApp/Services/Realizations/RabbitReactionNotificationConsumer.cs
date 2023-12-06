using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        public RabbitReactionNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewActiveReactionNotificationHub> activeHub,
            IHubContext<NewPassiveReactionNotificationHub> passiveHub,
            ILogger<RabbitReactionNotificationConsumer> logger,
            IOptions<QueueNamesConfiguration> queueNames,
            IWatchingPostCacheRepository cache)
        {
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
        }

        protected override Task ExecuteAsync(CancellationToken cancToken)
        {
            if(!cancToken.IsCancellationRequested)
            {
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

                    var createdReactionConsumer = new AsyncEventingBasicConsumer(_channel);

                    createdReactionConsumer.Received += async (model, ea) =>
                    {
                        var data = ea.Body.ToArray();
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
                    };

                    var removedReactionConsumer = new AsyncEventingBasicConsumer(_channel);

                    removedReactionConsumer.Received += async (model, ea) =>
                    {
                        var data = ea.Body.ToArray();

                        string message = Encoding.UTF8.GetString(data);

                        var reactionDto = JsonConvert.DeserializeObject<ReactionDto>(message);

                        var postWatchers = await _cache.GetPostWatchersUserIdsByPostId(reactionDto.PostId!.Value.ToString());

                        foreach (var watcher in postWatchers)
                        {
                            await _activeReactionHubContext.Clients
                            .Users(watcher)
                            .SendAsync("RemoveReaction", watcher, message);
                        }
                    };

                    _channel.BasicConsume(queue: _queueNames.Value.CreatedReactionEventQueue, autoAck: true, consumer: createdReactionConsumer);
                    _channel.BasicConsume(queue: _queueNames.Value.RemovedReactionEventQueue, autoAck: true, consumer: removedReactionConsumer);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}
