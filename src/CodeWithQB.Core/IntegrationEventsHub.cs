using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CodeWithQB.Core
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class IntegrationEventsHub: Hub { }
}
