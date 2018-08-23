using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class AddressScenarios: AddressScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateAddressCommand.Request, CreateAddressCommand.Response>(Post.Addresses, new CreateAddressCommand.Request() {
                        Address = new AddressDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            //var entity = context.Addresses.First();

                //Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetAddressesQuery.Response>(Get.Addresses);

                Assert.True(response.Addresses.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetAddressByIdQuery.Response>(Get.AddressById(1));

                Assert.True(response.Address.AddressId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetAddressByIdQuery.Response>(Get.AddressById(1));

                Assert.True(getByIdResponse.Address.AddressId != default(Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<UpdateAddressCommand.Request, UpdateAddressCommand.Response>(Post.Addresses, new UpdateAddressCommand.Request()
                    {
                        Address = getByIdResponse.Address
                    });

                Assert.True(saveResponse.AddressId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Address(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
