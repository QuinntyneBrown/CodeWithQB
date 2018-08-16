using CodeWithQB.Core.Interfaces;
using CodeWithQB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeWithQB.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {                
        public static IServiceCollection AddDataStore(this IServiceCollection services,
                                               string connectionString, bool useInMemoryDatabase = false)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();

            return services.AddDbContext<AppDbContext>(options =>
            {
                _ = useInMemoryDatabase 
                ? options.UseInMemoryDatabase(databaseName: "CodeWithQB")
                : options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CodeWithQB.Infrastructure"));
            });          
        }
    }
}
