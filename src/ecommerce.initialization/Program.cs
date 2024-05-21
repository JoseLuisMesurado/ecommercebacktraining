using ecommerce.infra;
using ecommerce.infra.Contexts;
using ecommerce.initialization;
using ecommerce.seeds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NG.EF.Common;
using NG.EF.Common.Helpers;

Console.WriteLine($"Starting Migration Proccess");
var host = CreateHostBuilder(args).Build();
await host.RunAsync();
Environment.Exit(0);

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {

            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapperHelper.UnhandledExceptionTrapper;
            Console.WriteLine($"Service Injection");
            var migrationConfiguration = new EFConfig
            {
                ConnectionString = @"Data Source=localhost; Initial Catalog=Ecommerce; Integrated Security=True; TrustServerCertificate=True;",
                MigrationAssembly = "ecommerce.sqlservermigration",
                DatabaseProviderType = DatabaseType.SqlServer
            };
            services.AddRefitConfiguration();
            services.AddInfrastructureConfiguration(migrationConfiguration);
            services.InjectSQLServerRepositories();
            //services.AddHostedService<MigrationBackgroundService<ECommerceDbContext, EcommerceSeedReference>>();
            services.AddHostedService<MigrationHttpBackgroundService<ECommerceDbContext, EcommerceSeedReference>>();
        });


}