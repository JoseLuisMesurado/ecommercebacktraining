using NG.EF.Common.BaseEntities;
using NG.EF.Common.BaseQueries;
using System.Linq.Expressions;

namespace NG.EF.Common.BaseRepositories
{
    public interface IBaseRepository<T, M> where T : class, IBaseEntity
    {
        IQueryable<T> FindWithSpecificationPattern(ISpecification<T>? specification = null);//pending to change IEnumerable Sucks
        Task<ICollection<TType>> Get<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select) where TType : class;
        Task<ICollection<TType>> GetAll<TType>(Expression<Func<T, TType>> select) where TType : class;
        Task<List<T>> GetAll();
        Task<T> FindBy(M entityId);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> LogicalDelete(string id);
    }
}
