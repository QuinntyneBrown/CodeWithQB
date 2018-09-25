using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class GalleryPhoto: Entity
    {
        public GalleryPhoto(string name)
            => Apply(new GalleryPhotoCreated(GalleryPhotoId));

        public Guid GalleryPhotoId { get; set; } = Guid.NewGuid();          
		public Guid GalleryId { get; set; }
        public Guid PhotoId { get; set; }
        public int Order { get; set; }
		public GalleryPhotoStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case GalleryPhotoCreated galleryPhotoCreated:
					GalleryPhotoId = galleryPhotoCreated.GalleryPhotoId;
					Status = GalleryPhotoStatus.Active;
                    break;

                case GalleryPhotoRemoved galleryPhotoRemoved:
                    Status = GalleryPhotoStatus.InActive;
					Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new GalleryPhotoRemoved());
    }

    public enum GalleryPhotoStatus
    {
        Active,
        InActive
    }
}
