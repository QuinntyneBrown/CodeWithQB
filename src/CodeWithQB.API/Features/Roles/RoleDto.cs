using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Roles
{
    public class RoleDto
    {        
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        public static RoleDto FromRole(Role role)
            => new RoleDto
            {
                RoleId = role.RoleId,
                Name = role.Name
            };
    }
}
