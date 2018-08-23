using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Events
{
    public class EventDto
    {        
        public Guid EventId { get; set; }
        public string Name { get; set; }

        public static EventDto FromEvent(Event @event)
            => new EventDto
            {
                EventId = @event.EventId,
                Name = @event.Name
            };
    }
}
