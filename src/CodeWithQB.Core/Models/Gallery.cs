using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class Gallery: Entity
    {
        public Gallery(string name)
            => Apply(new GalleryCreated(GalleryId, name));

        public Guid GalleryId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }
        public ICollection<Guid> GalleryPhotoIds { get; set; }
        public GalleryStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case GalleryCreated galleryCreated:
                    Name = galleryCreated.Name;
					GalleryId = galleryCreated.GalleryId;
					Status = GalleryStatus.Active;
                    break;

                case GalleryNameChanged galleryNameChanged:
                    Name = galleryNameChanged.Name;
					Version++;
                    break;

                case GalleryRemoved galleryRemoved:
                    Status = GalleryStatus.InActive;
					Version++;
                    break;
            }
        }
        

        public void Remove()
            => Apply(new GalleryRemoved());
    }

    public enum GalleryStatus
    {
        Active,
        InActive
    }
}
