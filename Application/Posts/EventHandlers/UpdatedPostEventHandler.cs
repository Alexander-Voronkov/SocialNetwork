using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Posts.EventHandlers
{
    public class UpdatedPostEventHandler : INotificationHandler<UpdatedPostEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<UpdatedPostEventHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatedPostEventHandler(IUnitOfWork unitOfWork, IEventBusSender sender, ILogger<UpdatedPostEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdatedPostEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation("Post was updated with id " + notification.Event.Id);
        }
    }
}
