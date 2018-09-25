using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class PhotoNameChanged: DomainEvent
    {
        public PhotoNameChanged(Guid photoNameChangedId, string name)
        {
            PhotoNameChangedId = photoNameChangedId;
            Name = name;
        }

		public Guid PhotoNameChangedId { get; set; }
        public string Name { get; set; }
    }
}
