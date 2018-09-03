using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Identity
{
    public interface ISecurityTokenFactory
    {
        string Create(Guid userId, string uniqueName, IEnumerable<string> roles = null);
    }
}
