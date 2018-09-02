using CodeWithQB.Core.Common;
using CodeWithQB.Core.Identity;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using CodeWithQB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace CodeWithQB.API
{
    public class AppInitializer: IDesignTimeDbContextFactory<AppDbContext>
    {
        public static void Seed(AppDbContext context, IServiceScopeFactory services)
        {            
            var eventStore = new EventStore(null, new MachineDateTime(), null, services);

            CardConfiguration.Seed(eventStore);
            RoleConfiguration.Seed(eventStore);
            UserConfiguration.Seed(eventStore);
            
            context.SaveChanges();
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)
                .Build();

            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"])
                .Options);
        }
    }


    internal class UserConfiguration
    {
        public static void Seed(IEventStore eventStore)
        {
            if (eventStore.Query<User>().SingleOrDefault(x => x.Username == "quinntynebrown@gmail.com") == null)
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var hashedPassword = new PasswordHasher().HashPassword(salt, "P@ssw0rd");

                var user = new User("quinntynebrown@gmail.com", salt, hashedPassword);

                var adminRole = new Role("Admin");

                user.AddRole(adminRole.RoleId);

                var dashboard = new Dashboard("Default", user.UserId);

                eventStore.Save(user);
                eventStore.Save(dashboard);
            }
        }
    }

    internal class RoleConfiguration
    {
        public static void Seed(IEventStore eventStore)
        {
            if (eventStore.Query<Role>().SingleOrDefault(x => x.Name == "Admin") == null)
                eventStore.Save(new Role("Admin"));

            if (eventStore.Query<Role>().SingleOrDefault(x => x.Name == "Mentee") == null)
                eventStore.Save(new Role("Mentee"));
        }
    }

    internal class CardConfiguration
    {
        public static void Seed(IEventStore eventStore)
        {
            if (eventStore.Query<Card>().SingleOrDefault(x => x.Name == "Events") == null)
                eventStore.Save(new Card("Events"));

            if (eventStore.Query<Card>().SingleOrDefault(x => x.Name == "Mentees") == null)
                eventStore.Save(new Card("Mentees"));
        }
    }

    internal class DashboardConfiguration
    {
        public static void Seed(IEventStore eventStore)
        {
        }
    }

    internal class DashboardTileConfiguration {
        public static void Seed(IEventStore eventStore)
        {
        }
    }

    internal class ProductConfiguration
    {
        public static void Seed(IEventStore eventStore)
        {
            if (eventStore.Query<Product>().SingleOrDefault(x => x.Name == "Mentoring") == null)
            {
                eventStore.Save(new Product("Mentoring",300,""));
            }
        }
    }
}
