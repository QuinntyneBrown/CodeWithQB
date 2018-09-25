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
        public static void Seed(
            IDateTime dateTime, 
            IEventStore eventStore, 
            IServiceScopeFactory services, 
            IRepository repository)
        {
            CardConfiguration.Seed(dateTime, eventStore, repository);
            RoleConfiguration.Seed(dateTime, eventStore, repository);
            UserConfiguration.Seed(dateTime, eventStore, repository);
            ProductConfiguration.Seed(dateTime, eventStore, repository);
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
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
            if (repository.Query<User>().SingleOrDefault(x => x.Username == "quinntynebrown@gmail.com") == null)
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var hashedPassword = new PasswordHasher().HashPassword(salt, "P@ssw0rd");

                var user = new User("quinntynebrown@gmail.com", salt, hashedPassword);

                var adminRole = repository.Query<Role>().Where(x => x.Name == "Admin").Single();

                user.AddRole(adminRole.RoleId);

                var dashboard = new Dashboard("Default", user.UserId);

                eventStore.Save(user);
                eventStore.Save(dashboard);
            }

            if (repository.Query<User>().SingleOrDefault(x => x.Username == "ericevans@domainlanguage.com") == null)
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(salt);

                var hashedPassword = new PasswordHasher().HashPassword(salt, "P@ssw0rd");

                var user = new User("ericevans@domainlanguage.com", salt, hashedPassword);

                var adminRole = repository.Query<Role>().Where(x => x.Name == "Mentee").Single(); 

                user.AddRole(adminRole.RoleId);

                var dashboard = new Dashboard("Default", user.UserId);

                eventStore.Save(user);
                eventStore.Save(dashboard);
            }
        }
    }
    

    internal class RoleConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
            if (repository.Query<Role>().SingleOrDefault(x => x.Name == "Admin") == null)
                eventStore.Save(new Role("Admin"));

            if (repository.Query<Role>().SingleOrDefault(x => x.Name == "Mentee") == null)
                eventStore.Save(new Role("Mentee"));

            if (repository.Query<Role>().SingleOrDefault(x => x.Name == "Customer") == null)
                eventStore.Save(new Role("Customer"));
        }
    }

    internal class CardConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
            if (repository.Query<Card>().SingleOrDefault(x => x.Name == "Events") == null)
                eventStore.Save(new Card("Events"));

            if (repository.Query<Card>().SingleOrDefault(x => x.Name == "Mentees") == null)
                eventStore.Save(new Card("Mentees"));
        }
    }

    internal class DashboardConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
        }
    }

    internal class DashboardTileConfiguration {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
        }
    }

    internal class ProductConfiguration
    {
        public static void Seed(IDateTime dateTime, IEventStore eventStore, IRepository repository)
        {
            if (repository.Query<Product>().SingleOrDefault(x => x.Name == "Mentoring") == null)
                eventStore.Save(new Product("Mentoring", 300, "<p>I provide remote mentoring in area of Software Development to all ages and levels of experience.</p>"));

            if (repository.Query<Product>().SingleOrDefault(x => x.Name == "Training") == null)
                eventStore.Save(new Product("Training", 300, "<p>I provide invidual training and group training</p>"));

            if (repository.Query<Product>().SingleOrDefault(x => x.Name == "Assessments") == null)
                eventStore.Save(new Product("Assessments", 300, "<p>I provide assements of Developer skills.</p>"));
        }
    }
}
