using CodeWithQB.Core.Models;

namespace CodeWithQB.Core.Common
{
    public class EventStoreChanged
    {
        public EventStoreChanged(StoredEvent @event) => Event = @event;
        public StoredEvent Event { get; }
    }
}
