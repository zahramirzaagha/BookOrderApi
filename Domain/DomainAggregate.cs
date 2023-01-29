using MediatR;

namespace Domain
{
    /// <summary>
    /// Base class for domain aggregates:
    /// Aggreagates are domain objects.
    /// The relationship between an aggregate and the domain objects inside of it is of composition type(not containment). Meaning that the constituents won't exist (don't have any meaning) without the aggregate.
    /// The constituents are persisted/deleted as part of the aggregate for the same reason as above.
    /// Aggregates and constituents can depend to other aggregates but constituents should never depend on the constituents in other aggregates.
    /// </summary>
    public abstract class DomainAggregate
    {
        private List<INotification> _domainEvents;

        public DomainAggregate()
        {
            _domainEvents = new List<INotification>();
        }

        /// <summary>
        /// Adds domain event the the list of events inside an aggregate
        /// </summary>
        /// <param name="eventItem">
        /// The domain event to be added
        /// </param>
        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes the domain events from the list and dispatches them one by one
        /// </summary>
        /// <param name="mediator">
        /// The mediator used to dispatch domain event to the listener (interested party)
        /// </param>
        /// <param name="cancellationToken">
        /// The token used to cancel the operation at any time
        /// </param>
        /// <returns></returns>
        public async Task DispatchDomainEventsAsync(IMediator mediator, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in _domainEvents.ToList())
            {
                _domainEvents.Remove(domainEvent);
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
