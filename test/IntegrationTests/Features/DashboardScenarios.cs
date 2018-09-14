using CodeWithQB.API.Features.Dashboards;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class DashboardScenarios: DashboardScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateDashboardCommand.Request, CreateDashboardCommand.Response>(Post.Dashboards, new CreateDashboardCommand.Request() {
                        Dashboard = new DashboardDto()
                        {
                            Name = "Name",
                        }
                    });
     
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {

            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var repository = server.Host.Services.GetService(typeof(IRepository)) as IRepository;
                var dashboardId = repository.Query<Dashboard>().First().DashboardId;
                var client = server.CreateClient();
                var response = await client.GetAsync<GetDashboardByIdQuery.Response>(Get.DashboardById(dashboardId));
                var dashboard = response.Dashboard;

                Assert.True(dashboard.DashboardId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {

            }
        }
    }
}
