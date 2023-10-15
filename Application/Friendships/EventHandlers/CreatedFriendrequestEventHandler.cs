using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Friendships.EventHandlers
{
    public class CreatedFriendrequestEventHandler : INotificationHandler<CreatedFriendrequestEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedFriendrequestEventHandler> _logger;
        public CreatedFriendrequestEventHandler(IEventBusSender sender, ILogger<CreatedFriendrequestEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(CreatedFriendrequestEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation(
                "New friendrequest was created with id " 
                + notification.Event.Id
                + " between user "
                + notification.Event.FirstUserId
                + " and user " + notification.Event.SecondUserId);
        }
    }
}
