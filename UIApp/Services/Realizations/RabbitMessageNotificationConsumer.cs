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
        private readonly Semaphore _semaphore;
        private readonly IWatchingChatCacheRepository _cache;
        public RabbitMessageNotificationConsumer(
            IOptions<RabbitMQConfiguration> rabbitConfig,
            IHubContext<NewActiveMessageNotificationHub> activeHub,
            IHubContext<NewPassiveMessageNotificationHub> pasiveHub,
            IOptions<QueueNamesConfiguration> queueNames,
            ILogger<RabbitMessageNotificationConsumer> logger,
            IWatchingChatCacheRepository cache)
        {
            _semaphore = new Semaphore(10, 10);

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

        protected override async Task ExecuteAsync(CancellationToken cancToken)
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

            _logger.LogInformation(" --- Rabbit message consumer started to consume. --- ");

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

                BasicGetResult? createdMessageResult;
                BasicGetResult? updatedMessageResult;
                BasicGetResult? removedMessageResult;
               

                do
                {
                    createdMessageResult = _channel.BasicGet(_queueNames.Value.CreatedMessageEventQueue, false);
                    updatedMessageResult = _channel.BasicGet(_queueNames.Value.UpdatedMessageEventQueue, false);
                    removedMessageResult = _channel.BasicGet(_queueNames.Value.RemovedMessageEventQueue, false);
                    
                    if (createdMessageResult != null)
                    {
                        var data = createdMessageResult.Body.ToArray();
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

                        _channel.BasicAck(createdMessageResult.DeliveryTag, false);
                    }
                    if (updatedMessageResult != null)
                    {
                        var data = updatedMessageResult.Body.ToArray();
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

                        _channel.BasicAck(updatedMessageResult.DeliveryTag, false);
                    }

                    if (removedMessageResult != null)
                    {
                        var data = removedMessageResult.Body.ToArray();
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

                        _channel.BasicAck(removedMessageResult.DeliveryTag, false);
                    }
                }
                while ((createdMessageResult != null || updatedMessageResult != null || removedMessageResult != null) && !cancToken.IsCancellationRequested);
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
