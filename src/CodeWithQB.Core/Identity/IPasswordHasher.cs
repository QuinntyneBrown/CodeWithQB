using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWithQB.Core.Identity
{
    public interface IPasswordHasher
    {
        string HashPassword(Byte[] salt, string password);
    }
}
