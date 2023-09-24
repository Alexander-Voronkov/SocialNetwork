using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Comments.EventHandlers
{
    public class CreatedCommentEventHandler : INotificationHandler<CreatedCommentEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedCommentEventHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreatedCommentEventHandler(IUnitOfWork unitOfWork, IEventBusSender sender, ILogger<CreatedCommentEventHandler> logger) 
        {
            _sender = sender;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreatedCommentEvent notification, CancellationToken cancellationToken)
        {
            var post = await (await _unitOfWork.PostsRepository.GetAll()).Where(x=>x.Id == notification.Event.PostId).Select(x=>new { x.OwnerId }).FirstOrDefaultAsync();
            
            notification.Event.Post = new Domain.Entities.Post() { OwnerId = post!.OwnerId };
            _logger.LogInformation(
                "New comment was writted with id " +
                notification.Event.Id +
                " under post " +
                notification.Event.PostId);

            await _sender.Send(notification);
        }
    }
}
