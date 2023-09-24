using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Friendrequests.EventHandlers
{
    public class CreatedFriendrequestEventHandler : INotificationHandler<CreatedFriendrequestEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedFriendrequestEventHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreatedFriendrequestEventHandler(IUnitOfWork unitOfWork, IEventBusSender sender, ILogger<CreatedFriendrequestEventHandler> logger) 
        {
            _sender = sender;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreatedFriendrequestEvent notification, CancellationToken cancellationToken)
        {
            var from = await _unitOfWork.UsersRepository.Get(notification.Event.Id);
            notification.Event.From = from;
            await _sender.Send(notification);
            _logger.LogInformation(
                "New friendrequest was created with id " +
                notification.Event.Id + 
                " between user " +
                notification.Event.FromUserId +
                " and user " + 
                notification.Event.ToUserId);
        }
    }
}
