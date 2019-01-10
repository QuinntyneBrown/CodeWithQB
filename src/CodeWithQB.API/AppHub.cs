using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CodeWithQB.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AppHub: Hub {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("On Connected Async");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("On Disconnected Async");

            return base.OnDisconnectedAsync(exception);
        }
    }
}
