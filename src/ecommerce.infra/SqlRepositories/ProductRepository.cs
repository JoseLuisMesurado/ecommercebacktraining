using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using ecommerce.infra.Contexts;
using NG.EF.Common.BaseRepositories;

namespace ecommerce.infra.SqlRepositories
{
    public class ProductRepository : Repository<Product<Guid>, ECommerceDbContext, Guid>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
