using CodeWithQB.API.Features.Cards;
using CodeWithQB.Core.Models;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class CardScenarios: CardScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {

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

            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                IRepository repository = server.Host.Services.GetService(typeof(IRepository)) as IRepository;

                var card = repository.Query<Card>().First();

                var client = server.CreateClient();

                await client.PutAsAsync<UpdateCardCommand.Request, UpdateCardCommand.Response>(Post.Cards, new UpdateCardCommand.Request()
                {
                    Card = CardDto.FromCard(card)
                });                
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
