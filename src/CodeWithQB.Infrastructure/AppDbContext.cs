using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeWithQB.Infrastructure
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options) { }

        public DbSet<Address> Addresses { private set; get; }
        public DbSet<Customer> Customers { private set; get; }
        public DbSet<DigitalAsset> DigitalAssets { private set; get; }
        public DbSet<Location> Locations { private set; get; }
        public DbSet<Talk> Talks { private set; get; }
        public DbSet<User> Users { private set; get; }
    }
}
