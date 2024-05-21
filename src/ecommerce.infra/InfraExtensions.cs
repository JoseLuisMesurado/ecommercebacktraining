using ecommerce.core.IContexts;
using ecommerce.core.ISqlRepositories;
using ecommerce.infra.Contexts;
using ecommerce.infra.SqlRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NG.EF.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, EFConfig efconfigoptions)
        {
            Console.WriteLine(efconfigoptions);
            try
            {
                switch (efconfigoptions.DatabaseProviderType)
                {
                    case DatabaseType.SqlServer:
                        services.AddDbContext<IECommerceDbContext, ECommerceDbContext>(options =>
                                    options.UseSqlServer(efconfigoptions.ConnectionString,
                                    optionsSql => optionsSql.MigrationsAssembly(efconfigoptions.MigrationAssembly)
                                    ));
                        //.UseLazyLoadingProxies());
                        break;
                    //case DatabaseProviderType.MySql:
                    //    services.AddDbContext<IIdentityContext, IdentityContext>(options =>
                    //                options.UseMySql(connectionString,
                    //                ServerVersion.AutoDetect(connectionString),
                    //                optionsSql => optionsSql.MigrationsAssembly("IOL.IDS4.UserManagement.MySql")
                    //                ).UseLazyLoadingProxies());
                    //    break;
                    default:
                        throw new ArgumentOutOfRangeException(DatabaseType.SqlServer, $@"The value needs to be one of {string.Join(", ", DatabaseType.SqlServer)}.");
                }
                //services.InjectSQLServerRepositories();
                return services;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static IServiceCollection InjectSQLServerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            return services;
        }
    }
}
