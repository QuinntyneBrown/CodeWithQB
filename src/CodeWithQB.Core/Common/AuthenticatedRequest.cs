using CodeWithQB.Core.Interfaces;
using MediatR;
using System;

namespace CodeWithQB.Core.Common
{
    public class AuthenticatedRequest<TResponse> : IAuthenticatedRequest, IRequest<TResponse>
    {
        public Guid CurrentUserId { get; set; }
    }
}
