using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWithQB.Core.Models
{
    public class User: AggregateRoot
    {
        public User(string username = null, byte[] salt= null, string password = null) 
            => Apply(new UserCreated(UserId, username,salt,password));

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case UserCreated data:
                    UserId = data.UserId;
                    Username = data.Username;
                    Salt = data.Salt;
                    Password = data.Password;
                    RoleIds = new HashSet<Guid>();
                    break;

                case UserRoleAdded userRoleAdded:
                    RoleIds = RoleIds.Concat(new Guid[] { userRoleAdded.RoleId }).ToList();
                    break;
            }            
        }

        protected override void EnsureValidState()
        {

        }

        public void AddRole(Guid roleId)
            => Apply(new UserRoleAdded(roleId));

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; private set; }
        public ICollection<Guid> RoleIds { get; private set; }
        public UserStatus Status { get; private set; }
    }

    public enum UserStatus {

    }
}
