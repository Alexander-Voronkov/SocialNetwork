using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Friendships.EventHandlers
{
    public class AcceptedFriendshipEventHandler : INotificationHandler<AcceptedFriendshipEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<AcceptedFriendshipEventHandler> _logger;
        public AcceptedFriendshipEventHandler(IEventBusSender sender, ILogger<AcceptedFriendshipEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(AcceptedFriendshipEvent notification, CancellationToken cancellationToken)
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
