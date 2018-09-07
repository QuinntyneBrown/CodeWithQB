using CodeWithQB.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Behaviours
{
    public class AuthenticatedRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedRequestBehavior(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;
        
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            switch (request)
            {
                case IAuthenticatedRequest authenticatedRequest:
                    authenticatedRequest.CurrentUserId = new Guid(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));
                    break;
            }

            return next();
        }
    }
}
