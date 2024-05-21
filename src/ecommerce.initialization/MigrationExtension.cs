using ecommerce.seeds.HttpRequest;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ecommerce.initialization
{
    public static class MigrationExtension
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services)
        {
            services.AddRefitClient<IHttpRequestMigration>()
               .ConfigureHttpClient(client =>
               {
                   client.BaseAddress = new Uri("https://dummyjson.com");
               });
            return services;
        }
    }
}
