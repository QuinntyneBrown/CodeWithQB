using CodeWithQB.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
