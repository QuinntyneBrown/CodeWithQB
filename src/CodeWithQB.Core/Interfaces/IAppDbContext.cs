using CodeWithQB.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<ContactRequest> ContactRequests { get; }
        DbSet<Course> Courses { get; }
        DbSet<Customer> Customers { get; }
        DbSet<DigitalAsset> DigitalAssets { get; }
        DbSet<Location> Locations { get; }
        DbSet<Mentor> Mentors { get; }
        DbSet<Product> Products { get; }
        DbSet<Tag> Tags { get; }
        DbSet<Talk> Talks { get; }
        DbSet<User> Users { get; }
        DbSet<Video> Videos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
