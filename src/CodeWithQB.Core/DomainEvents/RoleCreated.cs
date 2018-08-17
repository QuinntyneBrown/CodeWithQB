using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class RoleCreated: DomainEvent
    {
        public RoleCreated(Guid roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
    }
}
