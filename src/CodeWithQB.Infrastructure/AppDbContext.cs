using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeWithQB.Infrastructure
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options)
        
        { }

        public DbSet<Customer> Customers { get; private set; }
        public DbSet<User> Users { get; private set; }
    }
}
