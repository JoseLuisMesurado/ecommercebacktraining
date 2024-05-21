using ecommerce.core.Entities;
using NG.EF.Common.BaseRepositories;

namespace ecommerce.core.ISqlRepositories
{
    public interface ICategoryRepository : IRepository<Category<byte>, byte>
    {
    }
}
