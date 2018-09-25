using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ServiceProviderNameChanged: DomainEvent
    {
        public ServiceProviderNameChanged(Guid serviceProviderNameChangedId, string name)
        {
            ServiceProviderNameChangedId = serviceProviderNameChangedId;
            Name = name;
        }

		public Guid ServiceProviderNameChangedId { get; set; }
        public string Name { get; set; }
    }
}
