using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using ecommerce.infra.Contexts;
using NG.EF.Common.BaseRepositories;

namespace ecommerce.infra.SqlRepositories
{
    public class CategoryRepository : Repository<Category<byte>, ECommerceDbContext, byte>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
