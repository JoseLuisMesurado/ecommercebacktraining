using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using ecommerce.infra.Contexts;
using NG.EF.Common.BaseRepositories;

namespace ecommerce.infra.SqlRepositories
{
    public class ProductImageRepository : Repository<ProductImage<Guid>, ECommerceDbContext, Guid>, IProductImageRepository
    {
        public ProductImageRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
