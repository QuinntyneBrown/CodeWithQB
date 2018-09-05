using CodeWithQB.API.Features.ShoppingCarts;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
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
                IEventStore eventStore = server.Host.Services.GetService(typeof(IEventStore)) as IEventStore;
                var client = server.CreateClient();

                var product = eventStore.Query<Product>().First();

                var response = await client
                    .PostAsAsync<CreateShoppingCartItemCommand.Request, CreateShoppingCartItemCommand.Response>(Post.ShoppingCartItems, new CreateShoppingCartItemCommand.Request() {
                        ShoppingCartItem = new ShoppingCartItemDto()
                        {
                            ProductId = product.ProductId
                        }
                    });

                Assert.True(response.ShoppingCartItemId != default(Guid));
                Assert.True(response.ShoppingCartId != default(Guid));

                var cart = await client.GetAsync<GetShoppingCartByIdQuery.Response>(Get.ShoppingCartById(response.ShoppingCartId));

                Assert.Single(cart.ShoppingCart.ShoppingCartItemIds);

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
                    .GetAsync<GetShoppingCartItemByIdQuery.Response>(Get.ShoppingCartItemById(Guid.NewGuid()));

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
