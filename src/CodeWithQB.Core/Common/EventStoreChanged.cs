using CodeWithQB.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWithQB.Core.Common
{
    public class EventStoreChanged
    {
        public EventStoreChanged(StoredEvent @event)
        {
            Event = @event;
        }
        public StoredEvent Event { get; }
    }
}
