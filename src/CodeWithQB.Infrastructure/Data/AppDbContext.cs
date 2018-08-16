using System.Threading;
using System.Threading.Tasks;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CodeWithQB.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options) { }

        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}
