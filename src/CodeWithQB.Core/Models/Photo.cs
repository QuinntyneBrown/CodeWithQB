using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Photo: Entity
    {
        public Photo(string name)
            => Apply(new PhotoCreated(PhotoId, name));

        public Guid PhotoId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public PhotoStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case PhotoCreated photoCreated:
                    Name = photoCreated.Name;
					PhotoId = photoCreated.PhotoId;
					Status = PhotoStatus.Active;
                    break;

                case PhotoNameChanged photoNameChanged:
                    Name = photoNameChanged.Name;
					Version++;
                    break;

                case PhotoRemoved photoRemoved:
                    Status = PhotoStatus.InActive;
					Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new PhotoRemoved());
    }

    public enum PhotoStatus
    {
        Active,
        InActive
    }
}
