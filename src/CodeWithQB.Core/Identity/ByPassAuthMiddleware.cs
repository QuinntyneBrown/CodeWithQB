using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

// https://github.com/dotnet-architecture/eShopOnContainers/blob/c51c101f90fedaf224829e9ca73d4cfc54b69515/src/Services/Basket/Basket.API/Infrastructure/Middlewares/ByPassAuthMiddleware.cs

namespace CodeWithQB.Core.Identity
{
    public class ByPassAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISecurityTokenFactory _securityTokenFactory;

        public ByPassAuthMiddleware(ISecurityTokenFactory securityTokenFactory, RequestDelegate next) {
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
