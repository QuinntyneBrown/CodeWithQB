using CodeWithQB.Core.DomainEvents;
using System.Collections.Generic;

namespace CodeWithQB.Core.Common
{
    public abstract class AggregateRoot
    {
        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void RaiseDomainEvent(DomainEvent @event) {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(@event);
        }
        public void ClearEvents() => _domainEvents.Clear();
        public void Apply(DomainEvent @event)
        {
            When(@event);
            EnsureValidState();
            RaiseDomainEvent(@event);
        }
        protected abstract void When(DomainEvent @event);
        protected abstract void EnsureValidState();
    }
}
