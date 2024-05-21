using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using ecommerce.infra.Contexts;
using NG.EF.Common.BaseRepositories;

namespace ecommerce.infra.SqlRepositories
{
    public class BrandRepository : Repository<Brand<int>, ECommerceDbContext, int>, IBrandRepository
    {
        public BrandRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
