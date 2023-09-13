using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.EventHandlers
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
                "New friendrequest was created with id " +
                notification.Request.Id + 
                " between user " +
                notification.Request.FromUserId +
                " and user " + 
                notification.Request.ToUserId);
        }
    }
}
