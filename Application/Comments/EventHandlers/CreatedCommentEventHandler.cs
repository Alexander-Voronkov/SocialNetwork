using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.EventHandlers
{
    public class CreatedCommentEventHandler : INotificationHandler<CreatedCommentEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedCommentEventHandler> _logger;
        public CreatedCommentEventHandler(IEventBusSender sender, ILogger<CreatedCommentEventHandler> logger) 
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(CreatedCommentEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation(
                "New comment was writted with id " +
                notification.Comment.Id +
                " under post "+
                notification.Comment.PostId);
        }
    }
}
