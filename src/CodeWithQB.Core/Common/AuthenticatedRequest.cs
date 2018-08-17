using CodeWithQB.Core.Interfaces;
using System;

namespace CodeWithQB.Core.Common
{
    public class AuthenticatedRequest : IAuthenticatedRequest
    {
        public Guid CurrentUserId { get; set; }
    }
}
