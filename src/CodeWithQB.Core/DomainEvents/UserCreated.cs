using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class UserCreated: DomainEvent
    {
        public UserCreated(Guid userId, string username,byte[] salt, string password)
        {
            UserId = userId;
            Username = username;
            Salt = salt;
            Password = password;
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public string Password { get; set; }
    }
}
