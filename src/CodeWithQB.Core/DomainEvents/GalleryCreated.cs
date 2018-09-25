using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class GalleryCreated: DomainEvent
    {
        public GalleryCreated(Guid galleryId, string name)
        {
            GalleryId = galleryId;
            Name = name;
        }

		public Guid GalleryId { get; set; }
        public string Name { get; set; }
    }
}
