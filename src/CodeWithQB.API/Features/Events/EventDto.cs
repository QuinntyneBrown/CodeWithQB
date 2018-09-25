using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;
using System;
using System.Collections.Generic;

namespace CodeWithQB.API.Features.Events
{
    public class EventDto
    {        
        public Guid EventId { get; set; }
        public Guid? ParentEventId { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public IEnumerable<EventDto> Events { get; set; }
        public static EventDto FromEvent(Event @event)
            => new EventDto
            {
                EventId = @event.EventId,
                Name = @event.Name
            };
    }
}
