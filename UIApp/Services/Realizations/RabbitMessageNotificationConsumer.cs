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
    public class RabbitMessageNotificationConsumer : BackgroundService
    {
        private readonly IOptions<RabbitMQConfiguration> _rabbitConfig;
        private readonly IOptions<QueueNamesConfiguration> _queueNames;
        private readonly IHubContext<NewActiveMessageNotificationHub> _activeMessageHubContext;
        private readonly IHubContext<NewPassiveMessageNotificationHub> _passiveMessageHubContext;
        private readonly IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly ILogger<RabbitMessageNotificationConsumer> _logger;
        private readonly IWatchingChatCacheRepository _cache;
        public RabbitMessageNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewActiveMessageNotificationHub> activeHub,
            IHubContext<NewPassiveMessageNotificationHub> pasiveHub,
            IOptions<QueueNamesConfiguration> queueNames,
            ILogger<RabbitMessageNotificationConsumer> logger,
            IWatchingChatCacheRepository cache)
        {
            _cache = cache;

            _logger = logger;

            _rabbitConfig = rabbitConfig;

            _activeMessageHubContext = activeHub;

            _passiveMessageHubContext = pasiveHub;

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
                    _channel.QueueDeclare(queue: _queueNames.Value.CreatedMessageEventQueue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    _channel.QueueDeclare(queue: _queueNames.Value.UpdatedMessageEventQueue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    _channel.QueueDeclare(queue: _queueNames.Value.RemovedMessageEventQueue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    var createdMessageConsumer = new AsyncEventingBasicConsumer(_channel);

                    createdMessageConsumer.Received += async (model, ea) =>
                    {
                        var data = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(data);

                        _logger.LogInformation(" --- Rabbit created message consumer consumed a new message. --- ");

                        var messageDto = JsonConvert.DeserializeObject<MessageDto>(message);

                        var currentChatParticipants = await _cache.GetChatWatchersByChatId(messageDto.ChatId!.Value.ToString());

                        foreach (var participant in currentChatParticipants)
                        {
                            await _activeMessageHubContext.Clients
                                .User(participant)
                                .SendAsync("ReceiveChatMessage", participant, message);
                        }

                        if (!currentChatParticipants.Contains(messageDto.Chat!.FirstUserId!.Value.ToString()))
                            await _passiveMessageHubContext.Clients
                                .User(messageDto.Chat!.FirstUserId!.Value.ToString())
                                .SendAsync("ReceiveChatMessage", messageDto.Chat!.FirstUserId!.Value.ToString(), message);
                        else if (!currentChatParticipants.Contains(messageDto.Chat!.SecondUserId!.Value.ToString()))
                            await _passiveMessageHubContext.Clients
                                .User(messageDto.Chat!.SecondUserId!.Value.ToString())
                                .SendAsync("ReceiveChatMessage", messageDto.Chat!.SecondUserId!.Value.ToString(), message);
                    };

                    var updatedMessageConsumer = new AsyncEventingBasicConsumer(_channel);

                    updatedMessageConsumer.Received += async (model, ea) =>
                    {
                        var data = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(data);

                        _logger.LogInformation(" --- Rabbit updated message consumer consumed an updated message. --- ");

                        var messageDto = JsonConvert.DeserializeObject<MessageDto>(message);

                        var currentChatParticipants = await _cache.GetChatWatchersByChatId(messageDto.ChatId!.Value.ToString());

                        foreach (var participant in currentChatParticipants)
                        {
                            await _activeMessageHubContext.Clients
                                .User(participant)
                                .SendAsync("UpdateChatMessage", participant, message);
                        }
                    };

                    var removedMessageConsumer = new AsyncEventingBasicConsumer(_channel);

                    removedMessageConsumer.Received += async (model, ea) =>
                    {
                        var data = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(data);

                        _logger.LogInformation(" --- Rabbit updated message consumer consumed a deleted message. --- ");

                        var messageDto = JsonConvert.DeserializeObject<MessageDto>(message);

                        var currentChatParticipants = await _cache.GetChatWatchersByChatId(messageDto.ChatId!.Value.ToString());

                        foreach (var participant in currentChatParticipants)
                        {
                            await _activeMessageHubContext.Clients
                                    .User(participant)
                                    .SendAsync("RemoveChatMessage", participant, message);
                        }
                    };

                    _channel.BasicConsume(queue: _queueNames.Value.CreatedMessageEventQueue, autoAck: true, consumer: createdMessageConsumer);
                    _channel.BasicConsume(queue: _queueNames.Value.UpdatedMessageEventQueue, autoAck: true, consumer: updatedMessageConsumer);
                    _channel.BasicConsume(queue: _queueNames.Value.RemovedMessageEventQueue, autoAck: true, consumer: removedMessageConsumer);
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
