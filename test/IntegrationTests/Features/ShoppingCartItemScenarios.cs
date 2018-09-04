using CodeWithQB.API.Features.ShoppingCarts;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ShoppingCartItemScenarios: ShoppingCartItemScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateShoppingCartItemCommand.Request, CreateShoppingCartItemCommand.Response>(Post.ShoppingCartItems, new CreateShoppingCartItemCommand.Request() {
                        ShoppingCartItem = new ShoppingCartItemDto()
                        {

                        }
                    });
     
	            //var entity = context.ShoppingCartItems.First();

                //Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetShoppingCartItemsQuery.Response>(Get.ShoppingCartItems);

                Assert.True(response.ShoppingCartItems.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {

            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetShoppingCartItemByIdQuery.Response>(Get.ShoppingCartItemById(1));

                Assert.True(getByIdResponse.ShoppingCartItem.ShoppingCartItemId != default(Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<UpdateShoppingCartItemCommand.Request, UpdateShoppingCartItemCommand.Response>(Post.ShoppingCartItems, new UpdateShoppingCartItemCommand.Request()
                    {
                        ShoppingCartItem = getByIdResponse.ShoppingCartItem
                    });

                Assert.True(saveResponse.ShoppingCartItemId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.ShoppingCartItem(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
