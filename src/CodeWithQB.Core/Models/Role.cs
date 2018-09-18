using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Role: Entity
    {
        public Role(string name)
            => Apply(new RoleCreated(RoleId,name));

        public Guid RoleId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }        
        public RoleStatus Status { get; set; }
        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case RoleCreated roleCreated:
                    Name = roleCreated.Name;
                    RoleId = roleCreated.RoleId;
                    break;

                case RoleNameChanged roleNameChanged:
                    Name = roleNameChanged.Name;
                    break;

                case RoleRemoved roleRemoved:
                    Status = RoleStatus.InActive;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new RoleNameChanged(name));

        public void Remove()
            => Apply(new RoleRemoved());
    }

    public enum RoleStatus
    {
        Active,
        InActive
    }
}
