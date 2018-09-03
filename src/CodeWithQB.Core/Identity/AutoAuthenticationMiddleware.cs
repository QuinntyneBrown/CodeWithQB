using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Identity
{
    public class AutoAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISecurityTokenFactory _securityTokenFactory;

        public AutoAuthenticationMiddleware(ISecurityTokenFactory securityTokenFactory, RequestDelegate next) {
            _next = next;
            _securityTokenFactory = securityTokenFactory;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = _securityTokenFactory.Create(Guid.NewGuid(), "quinntynebrown@gmail.com");
            httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");
            await _next.Invoke(httpContext);            
        }
    }
}
