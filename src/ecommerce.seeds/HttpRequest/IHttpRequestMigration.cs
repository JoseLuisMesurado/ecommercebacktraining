using ecommerce.seeds.Dtos;
using Refit;

namespace ecommerce.seeds.HttpRequest
{
    public interface IHttpRequestMigration
    {
        [Get("/products/")]
        Task<ProductList> RequestMockProducts();
    }
}
