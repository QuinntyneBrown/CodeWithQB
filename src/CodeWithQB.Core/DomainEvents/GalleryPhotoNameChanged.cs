using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class GalleryPhotoNameChanged: DomainEvent
    {
        public GalleryPhotoNameChanged(Guid galleryPhotoNameChangedId, string name)
        {
            GalleryPhotoNameChangedId = galleryPhotoNameChangedId;
            Name = name;
        }

		public Guid GalleryPhotoNameChangedId { get; set; }
        public string Name { get; set; }
    }
}
