using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class ServiceProvider: Entity
    {
        public ServiceProvider(string name)
            => Apply(new ServiceProviderCreated(ServiceProviderId,name));

        public Guid ServiceProviderId { get; set; } = Guid.NewGuid();          		
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> ProductIds { get; set; }
        public ServiceProviderStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ServiceProviderCreated serviceProviderCreated:                    
					ServiceProviderId = serviceProviderCreated.ServiceProviderId;
					Status = ServiceProviderStatus.Active;
                    break;

                case ServiceProviderNameChanged serviceProviderNameChanged:
                    
					Version++;
                    break;

                case ServiceProviderRemoved serviceProviderRemoved:
                    Status = ServiceProviderStatus.InActive;
					Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new ServiceProviderRemoved());
    }

    public enum ServiceProviderStatus
    {
        Active,
        InActive
    }
}
