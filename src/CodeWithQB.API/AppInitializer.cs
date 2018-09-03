using CodeWithQB.Core.Common;
using CodeWithQB.Core.Identity;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using CodeWithQB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using static Newtonsoft.Json.JsonConvert;

namespace CodeWithQB.API
{
    public class AppInitializer: IDesignTimeDbContextFactory<AppDbContext>
    {
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore, IServiceScopeFactory services)
        {
            CardConfiguration.Seed(context, dateTime, eventStore);
            RoleConfiguration.Seed(context, dateTime, eventStore);
            UserConfiguration.Seed(context, dateTime, eventStore);
            ProductConfiguration.Seed(context, dateTime, eventStore);
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
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore)
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

                var adminRole = eventStore.Query<Role>().Where(x => x.Name == "Admin").Single();

                user.AddRole(adminRole.RoleId);

                var dashboard = new Dashboard("Default", user.UserId);

                AggregateHelper.Save(user, dateTime, context, eventStore);
                AggregateHelper.Save(dashboard, dateTime, context, eventStore);
            }

            if (eventStore.Query<User>().SingleOrDefault(x => x.Username == "ericevans@domainlanguage.com") == null)
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var hashedPassword = new PasswordHasher().HashPassword(salt, "P@ssw0rd");

                var user = new User("ericevans@domainlanguage.com", salt, hashedPassword);

                var adminRole = eventStore.Query<Role>().Where(x => x.Name == "Mentee").Single(); 

                user.AddRole(adminRole.RoleId);

                var dashboard = new Dashboard("Default", user.UserId);

                AggregateHelper.Save(user, dateTime, context, eventStore);
                AggregateHelper.Save(dashboard, dateTime, context, eventStore);
            }
        }
    }

    internal static class AggregateHelper
    {
        public static void Save<TAggregate>(TAggregate aggregate, IDateTime dateTime, AppDbContext context, IEventStore eventStore)
            where TAggregate : AggregateRoot
        {
            var type = aggregate.GetType();

            foreach (var @event in aggregate.DomainEvents)
            {
                context.Add(new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = aggregate.GetType().Name,
                    Data = SerializeObject(@event),
                    StreamId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregate, null),
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = dateTime.UtcNow,
                    Sequence = context.StoredEvents.Count() + 1
                });

                context.SaveChanges();
            }

            var aggregates = eventStore.UpdateState(type, aggregate, (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregate, null));

            Dictionary<string, IEnumerable<AggregateRoot>> data = new Dictionary<string, IEnumerable<AggregateRoot>>();

            foreach (var item in aggregates)
                data.Add(item.Key, item.Value);

            context.Snapshots.Add(new Snapshot()
            {
                AsOfDateTime = dateTime.UtcNow,
                Data = SerializeObject(data),
            });

            context.SaveChanges();
        }
    }

    internal class RoleConfiguration
    {
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore)
        {
            if (eventStore.Query<Role>().SingleOrDefault(x => x.Name == "Admin") == null)
                AggregateHelper.Save(new Role("Admin"), dateTime, context, eventStore);

            if (eventStore.Query<Role>().SingleOrDefault(x => x.Name == "Mentee") == null)
                AggregateHelper.Save(new Role("Mentee"), dateTime, context, eventStore);
        }
    }

    internal class CardConfiguration
    {
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore)
        {
            if (eventStore.Query<Card>().SingleOrDefault(x => x.Name == "Events") == null)
                AggregateHelper.Save(new Card("Events"), dateTime, context, eventStore);

            if (eventStore.Query<Card>().SingleOrDefault(x => x.Name == "Mentees") == null)
                AggregateHelper.Save(new Card("Mentees"), dateTime, context, eventStore);
        }
    }

    internal class DashboardConfiguration
    {
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore)
        {
        }
    }

    internal class DashboardTileConfiguration {
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore)
        {
        }
    }

    internal class ProductConfiguration
    {
        public static void Seed(AppDbContext context, IDateTime dateTime, IEventStore eventStore)
        {
            if (eventStore.Query<Product>().SingleOrDefault(x => x.Name == "Mentoring") == null)
                AggregateHelper.Save(new Product("Mentoring", 300, ""), dateTime, context, eventStore);

            if (eventStore.Query<Product>().SingleOrDefault(x => x.Name == "Training") == null)
                AggregateHelper.Save(new Product("Training", 300, ""), dateTime, context, eventStore);

            if (eventStore.Query<Product>().SingleOrDefault(x => x.Name == "Assessments") == null)
                AggregateHelper.Save(new Product("Assessments", 300, ""), dateTime, context, eventStore);

        }
    }
}
