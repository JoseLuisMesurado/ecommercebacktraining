using ecommerce.seeds.HttpRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NG.EF.Common.BaseSeeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.seeds
{
    public class MigrationHttpBackgroundService<T, D> : BackgroundService where T : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public MigrationHttpBackgroundService(
            IServiceProvider serviceProvider,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await RunDatabaseMigration(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("DB Migrations Service Hosted is stopping.");
            _hostApplicationLifetime.StopApplication();
            await base.StopAsync(stoppingToken);
        }

        public async Task RunDatabaseMigration(CancellationToken stoppingToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var currentContext = scope.ServiceProvider.GetRequiredService<T>();
            var httpRequestMigration = scope.ServiceProvider.GetRequiredService<IHttpRequestMigration>();

            Console.WriteLine($"Apply DB Migrations");
            var pendingMigrations = await currentContext.Database.GetPendingMigrationsAsync(cancellationToken: stoppingToken);
            if (pendingMigrations.Any())
            {
                Console.WriteLine($"You have {pendingMigrations.Count()} pending migrations to apply.");
                Console.WriteLine("Applying pending migrations now");
                await currentContext.Database.MigrateAsync(cancellationToken: stoppingToken);
            }
            try
            {
                var lastAppliedMigration = (await currentContext.Database.GetAppliedMigrationsAsync(cancellationToken: stoppingToken)).Last();
                Console.WriteLine($"You're on schema version: {lastAppliedMigration}");
            }
            catch (Exception ex)
            {
                throw;
            }

            Console.WriteLine($"Run Data Seed");
            await RunDataSeed(currentContext);
            await RunDataSeed(currentContext, httpRequestMigration);
            await currentContext.SaveChangesAsync(stoppingToken);
            await StopAsync(stoppingToken);
        }

        private static async Task RunDataSeed(T currentContext, IHttpRequestMigration httpRequestMigration)
        {
            try
            {
                var applicationAssembly = typeof(D).Assembly;
                var seeds = from t in applicationAssembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(ISqlServerMigrationSeed<T>))
                            && t.Namespace != null
                            select Activator.CreateInstance(t, httpRequestMigration) as ISqlServerMigrationSeed<T>;
                foreach (var seed in seeds)
                {
                    await seed.ExecuteAsync(currentContext);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task RunDataSeed(T currentContext)
        {
            var applicationAssembly = typeof(D).Assembly;
            var seeds = from t in applicationAssembly.GetTypes()
                        where t.GetInterfaces().Contains(typeof(ISqlServerMigrationSeed<T>))
                        && t.GetConstructor(Type.EmptyTypes) != null
                        && t.Namespace != null
                        select Activator.CreateInstance(t) as ISqlServerMigrationSeed<T>;

            foreach (var seed in seeds)
            {
                await seed.ExecuteAsync(currentContext);
            }
        }
    }
}
