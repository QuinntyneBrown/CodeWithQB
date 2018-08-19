using CodeWithQB.API.Features.Mentees;
using CodeWithQB.Core.Models;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using CodeWithQB.Core.DomainEvents;

namespace IntegrationTests.Features
{
    public class MenteeScenarios: MenteeScenarioBase
    {

        [Fact]
        public async Task ShouldRegister()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .PostAsync(Post.Register, new MenteeRegistrationRequested() {
                        FirstName = "Quinntyne",
                        LastName = "Brown",
                        Password ="P@ssw0rd",
                        EmailAddress ="quinntyne@hotmail.com"
                    });

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
