using System;

namespace CodeWithQB.Core.Interfaces
{
    public interface IAuthenticatedRequest
    {
        Guid CurrentUserId { get; set; }
    }
}
