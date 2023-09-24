using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Friendships.EventHandlers
{
    public class CreatedFriendshipEventHandler : INotificationHandler<CreatedFriendshipEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedFriendshipEventHandler> _logger;
        public CreatedFriendshipEventHandler(IEventBusSender sender, ILogger<CreatedFriendshipEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(CreatedFriendshipEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation(
                "New friendship was created with id " 
                + notification.Event.Id
                + " between user "
                + notification.Event.FirstUserId
                + " and user " + notification.Event.SecondUserId);
        }
    }
}
