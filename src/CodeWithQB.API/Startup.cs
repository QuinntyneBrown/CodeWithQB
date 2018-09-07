using FluentValidation.AspNetCore;
using CodeWithQB.Core;
using CodeWithQB.Core.Behaviours;
using CodeWithQB.Core.Extensions;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Identity;
using CodeWithQB.Infrastructure.Extensions;
using CodeWithQB.Infrastructure;
using CodeWithQB.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodeWithQB.Core.Common;
using System;

namespace CodeWithQB.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {                        
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddSingleton<IEventStore, EventStore>();
            services.AddSingleton<IRepository, Repository>();

            services.AddHttpContextAccessor();
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            services.AddCustomMvc()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services
                .AddCustomSecurity(Configuration)
                .AddCustomSignalR()
                .AddCustomSwagger()
                .AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"],Configuration.GetValue<bool>("isTest"))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthenticatedRequestBehavior<,>))
                .AddMediatR(typeof(Startup).Assembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            var repository = app.ApplicationServices.GetRequiredService<IRepository>() as IRepository;

            if(Configuration.GetValue<bool>("isTest"))
                app.UseMiddleware<ByPassAuthMiddleware>();
                    
            app.UseAuthentication()            
                .UseCors(CorsDefaults.Policy)            
                .UseMvc()
                .UseSignalR(routes => routes.MapHub<IntegrationEventsHub>("/hub"))
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeWithQB API");
                    options.RoutePrefix = string.Empty;
                });
        }        
    }


}
