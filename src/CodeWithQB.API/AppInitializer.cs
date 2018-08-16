using CodeWithQB.Core.Identity;
using CodeWithQB.Core.Models;
using CodeWithQB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Security.Cryptography;

namespace CodeWithQB.API
{
    public class AppInitializer: IDesignTimeDbContextFactory<AppDbContext>
    {
        public static void Seed(AppDbContext context)
        {
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
    
}
