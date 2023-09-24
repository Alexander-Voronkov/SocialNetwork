using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Reactions.EventHandlers
{
    public class CreatedReactionEventHandler : INotificationHandler<CreatedReactionEvent>
    {
        private readonly ILogger<CreatedReactionEventHandler> _logger;
        private readonly IEventBusSender _sender;
        private readonly IUnitOfWork _unitOfWork;
        public CreatedReactionEventHandler(IUnitOfWork unitOfWork, ILogger<CreatedReactionEventHandler> logger, IEventBusSender eventBus)
        {
            _logger = logger;
            _sender = eventBus;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreatedReactionEvent notification, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)notification.Event.PostId!);
            notification.Event.Post = post;
            await _sender.Send(notification);
            _logger.LogInformation("A new reaction was created with id " + notification.Event.Id);
        }
    }
}
