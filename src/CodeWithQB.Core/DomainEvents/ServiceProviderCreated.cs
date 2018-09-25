using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ServiceProviderCreated: DomainEvent
    {
        public ServiceProviderCreated(Guid serviceProviderId, string name)
        {
            ServiceProviderId = serviceProviderId;
            Name = name;
        }

		public Guid ServiceProviderId { get; set; }
        public string Name { get; set; }
    }
}
