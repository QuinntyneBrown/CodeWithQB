using CodeWithQB.API.Features.ShoppingCarts;
using CodeWithQB.Core.Models;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ShoppingCartScenarios: ShoppingCartScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IEventStore eventStore = server.Host.Services.GetService(typeof(IEventStore)) as IEventStore;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateShoppingCartCommand.Request, CreateShoppingCartCommand.Response>(Post.ShoppingCarts, new CreateShoppingCartCommand.Request() {
                        ShoppingCart = new ShoppingCartDto()
                        {

                        }
                    });

                var entity = eventStore.Query<ShoppingCart>().Single(x => x.ShoppingCartId == response.ShoppingCartId);

                Assert.True(entity.ShoppingCartId == response.ShoppingCartId);
            }
        }


        [Fact]
        public async Task ShouldFailToCreateShoppingCartItem()
        {
            using (var server = CreateServer())
            {
                IEventStore eventStore = server.Host.Services.GetService(typeof(IEventStore)) as IEventStore;
                var client = server.CreateClient();

                var product = eventStore.Query<Product>().First();

                var response = await client
                    .PostAsAsync<CreateShoppingCartItemCommand.Request, CreateShoppingCartItemCommand.Response>(Post.ShoppingCartItem(default(Guid)), new CreateShoppingCartItemCommand.Request()
                    {
                        ProductId = product.ProductId
                    });
                
                await Assert.ThrowsAsync<Exception>(async () => await client
                    .PostAsAsync<CreateShoppingCartItemCommand.Request, CreateShoppingCartItemCommand.Response>(Post.ShoppingCartItem(default(Guid)), new CreateShoppingCartItemCommand.Request()
                    {
                        ShoppingCartId = response.ShoppingCartId,
                        ProductId = product.ProductId
                    }));
            }
        }


        [Fact]
        public async Task ShouldCreateShoppingCartItem()
        {
            using (var server = CreateServer())
            {
                IEventStore eventStore = server.Host.Services.GetService(typeof(IEventStore)) as IEventStore;
                var client = server.CreateClient();

                var product = eventStore.Query<Product>().First();

                var response = await client
                    .PostAsAsync<CreateShoppingCartItemCommand.Request, CreateShoppingCartItemCommand.Response>(Post.ShoppingCartItem(default(Guid)), new CreateShoppingCartItemCommand.Request()
                    {
                        ProductId = product.ProductId
                    });

                Assert.True(response.ShoppingCartId != default(Guid));

                var cart = await client.GetAsync<GetShoppingCartByIdQuery.Response>(Get.ShoppingCartById(response.ShoppingCartId));

                Assert.Single(cart.ShoppingCart.ShoppingCartItems);

            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetShoppingCartsQuery.Response>(Get.ShoppingCarts);

                Assert.True(response.ShoppingCarts.Count() > 0);
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
                    .GetAsync<GetShoppingCartByIdQuery.Response>(Get.ShoppingCartById(default(Guid)));

                Assert.True(getByIdResponse.ShoppingCart.ShoppingCartId != default(Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<UpdateShoppingCartCommand.Request, UpdateShoppingCartCommand.Response>(Post.ShoppingCarts, new UpdateShoppingCartCommand.Request()
                    {
                        ShoppingCart = getByIdResponse.ShoppingCart
                    });

                Assert.True(saveResponse.ShoppingCartId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.ShoppingCart(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
