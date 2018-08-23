using CodeWithQB.API.Features.Events;
using CodeWithQB.Core.Models;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class EventScenarios: EventScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateEventCommand.Request, CreateEventCommand.Response>(Post.Events, new CreateEventCommand.Request() {
                        Event = new EventDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            //var entity = context.Events.First();

                //Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetEventsQuery.Response>(Get.Events);

                Assert.True(response.Events.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetEventByIdQuery.Response>(Get.EventById(1));

               // Assert.True(response.Event.EventId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetEventByIdQuery.Response>(Get.EventById(1));

                //Assert.True(getByIdResponse.Event.EventId != default(int));

                //var saveResponse = await server.CreateClient()
                //    .PostAsAsync<SaveEventCommand.Request, SaveEventCommand.Response>(Post.Events, new SaveEventCommand.Request()
                //    {
                //        Event = getByIdResponse.Event
                //    });

                //Assert.True(saveResponse.EventId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Event(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
