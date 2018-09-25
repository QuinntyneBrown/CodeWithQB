using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class GalleryPhotoCreated: DomainEvent
    {
        public GalleryPhotoCreated(Guid galleryPhotoId)
        {
            GalleryPhotoId = galleryPhotoId;            
        }

		public Guid GalleryPhotoId { get; set; }
        public string Name { get; set; }
    }
}
