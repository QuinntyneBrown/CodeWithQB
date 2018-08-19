using CodeWithQB.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CodeWithQB.Infrastructure.Data
{
    public static class DeserializedEventStore
    {
        public static ConcurrentDictionary<Guid, DeserializedStoredEvent> Events { get; set; }

        public static void TryAdd(StoredEvent @event)
            => Events.TryAdd(@event.StoredEventId, new DeserializedStoredEvent(@event));

        public static IEnumerable<DeserializedStoredEvent> Get()
        {
            var eventsCount = Events.Count();
            var deserializedStoredEvents = new DeserializedStoredEvent[eventsCount];
            for (var i = 0; i < eventsCount; i++)
                deserializedStoredEvents[i] = Events.ElementAt(i).Value;

            Array.Sort(deserializedStoredEvents, (x, y) => DateTime.Compare(x.CreatedOn, y.CreatedOn));

            return deserializedStoredEvents;
        }
    }
}
