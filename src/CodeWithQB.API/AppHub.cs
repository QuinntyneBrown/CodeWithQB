using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CodeWithQB.Api
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AppHub: Hub {

        private readonly ILogger<AppHub> _logger;

        public AppHub(ILogger<AppHub> logger) => _logger = logger;

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {            
            return base.OnDisconnectedAsync(exception);
        }
    }
}
