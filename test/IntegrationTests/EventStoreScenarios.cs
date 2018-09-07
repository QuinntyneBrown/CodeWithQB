using CodeWithQB.Core.Interfaces;
using CodeWithQB.Infrastructure.Data;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class EventStoreScenarios: ScenarioBase
    {
        [Fact]
        public async Task ShouldSaveEvent()
        {
            using (var server = CreateServer())
            {
                var context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;
                var eventStore = server.Host.Services.GetService(typeof(IEventStore)) as EventStore;
                
            }
        }
    }
}
