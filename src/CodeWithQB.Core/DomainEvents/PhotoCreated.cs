using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class PhotoCreated: DomainEvent
    {
        public PhotoCreated(Guid photoId, string name)
        {
            PhotoId = photoId;
            Name = name;
        }

		public Guid PhotoId { get; set; }
        public string Name { get; set; }
    }
}
