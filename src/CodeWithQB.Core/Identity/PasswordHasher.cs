using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace CodeWithQB.Core.Identity
{
    public class PasswordHasher: IPasswordHasher
    {
        public string HashPassword(Byte[] salt, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
