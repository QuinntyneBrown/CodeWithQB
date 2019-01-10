using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWithQB.Core.Identity
{
    public interface ISecurityTokenFactory
    {
        string Create(Guid userId, string uniqueName);
    }
}
