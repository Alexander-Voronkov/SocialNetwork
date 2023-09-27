using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Domain.Common;

namespace Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;
        private List<BaseEvent> _events;

        public DispatchDomainEventsInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            foreach (var domainEvent in _events)
                _mediator.Publish(domainEvent).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in _events)
                await _mediator.Publish(domainEvent);
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context != null)
            {
                var entities = eventData.Context.ChangeTracker
                    .Entries<Domain.Common.BaseEntity<int>>()
                    .Where(e => e.Entity.DomainEvents.Any())
                    .Select(e => e.Entity);

                _events = entities
                    .SelectMany(e => e.DomainEvents)
                    .ToList();


                entities.ToList().ForEach(e => e.ClearDomainEvents());
            }
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context != null)
            {
                var entities = eventData.Context.ChangeTracker
                    .Entries<Domain.Common.BaseEntity<int>>()
                    .Where(e => e.Entity.DomainEvents.Any())
                    .Select(e => e.Entity);

                _events = entities
                    .SelectMany(e => e.DomainEvents)
                    .ToList();


                entities.ToList().ForEach(e => e.ClearDomainEvents());
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
