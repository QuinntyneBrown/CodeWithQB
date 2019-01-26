using CodeWithQB.Core.Identity;
using CodeWithQB.Core.Models;
using CodeWithQB.Infrastructure;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;

namespace CodeWithQB.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            ProcessDbCommands(args, host);

            host.Run();
        }

        private static void ProcessDbCommands(string[] args, IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                if (args.Contains("ci"))
                    args = new string[4] { "dropdb", "migratedb", "seeddb", "stop" };

                if (args.Contains("dropdb"))
                    context.Database.EnsureDeleted();

                if (args.Contains("migratedb"))
                    context.Database.Migrate();

                if (args.Contains("seeddb"))
                {
                    context.Database.EnsureCreated();

                    User user = default(User);

                    if (context.Users.IgnoreQueryFilters().FirstOrDefault(x => x.Username == configuration["Seed:DefaultUser:Username"]) == null)
                    {
                        user = new User()
                        {
                            Username = configuration["Seed:DefaultUser:Username"]
                        };

                        user.Password = new PasswordHasher().HashPassword(user.Salt, configuration["Seed:DefaultUser:Password"]);

                        context.Users.Add(user);

                    }
                    
                    context.SaveChanges();
                }

                if (args.Contains("stop"))
                    Environment.Exit(0);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var builder = config.Build();

                var keyVaultEndpoint = $"https://codewithqb.vault.azure.net/";

                var azureServiceTokenProvider = new AzureServiceTokenProvider();

                var keyVaultClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback)
                    );

                config.AddAzureKeyVault(keyVaultEndpoint);                
            })
            .UseApplicationInsights()
            .UseSerilog((builderContext, config) =>
            {
                config
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.ApplicationInsightsTraces(new TelemetryClient());
            })
            .UseStartup<Startup>();
    }
}
