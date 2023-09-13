using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.EventHandlers
{
    public class CreatedReactionEventHandler : INotificationHandler<CreatedReactionEvent>
    {
        private readonly ILogger _logger;
        private readonly IEventBusSender _sender;
        public CreatedReactionEventHandler(ILogger<CreatedReactionEventHandler> logger, IEventBusSender eventBus)
        {
            _logger = logger;
            _sender = eventBus;
        }
        public async Task Handle(CreatedReactionEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation("A new reaction was created with id " + notification.Reaction.Id);
        }
    }
}
