using CodeWithQB.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Address> Addresses { get; }
        DbSet<Customer> Customers { get; }
        DbSet<DigitalAsset> DigitalAssets { get; }
        DbSet<Location> Locations { get; }
        DbSet<Talk> Talks { get; }
        DbSet<User> Users { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
