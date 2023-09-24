using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Reactions.EventHandlers
{
    public class RemovedReactionEventHandler : INotificationHandler<RemovedReactionEvent>
    {
        private readonly ILogger<RemovedReactionEventHandler> _logger;
        private readonly IEventBusSender _sender;
        private readonly IUnitOfWork _unitOfWork;

        public RemovedReactionEventHandler(IUnitOfWork unitOfWork, ILogger<RemovedReactionEventHandler> logger, IEventBusSender eventBus)
        {
            _logger = logger;
            _sender = eventBus;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemovedReactionEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation("Reaction was removed with id " + notification.Event.Id + " under post with id " + notification.Event.PostId);
        }
    }
}
