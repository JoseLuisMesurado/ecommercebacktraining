using Microsoft.EntityFrameworkCore;
using NG.EF.Common.BaseEntities;
using NG.EF.Common.BaseQueries;
using System.Linq.Expressions;

namespace NG.EF.Common.BaseRepositories
{
    public class BaseRepository<TEntity, TContext, M> : IBaseRepository<TEntity, M>
         where TEntity : class, IBaseEntity
         where TContext : DbContext
    {
        private readonly TContext _context;
        public BaseRepository(TContext context) => _context = context;

        public async Task<ICollection<TType>> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(where).Select(select).ToListAsync();
        }

        public async Task<ICollection<TType>> GetAll<TType>(Expression<Func<TEntity, TType>> selectExpression) where TType : class
        {
            return await _context.Set<TEntity>().AsNoTracking().Select(selectExpression).ToListAsync();
        }
        public async Task<List<TEntity>> GetAll()
        {
            var data = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return data;
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<TEntity?> FindBy(M entityId)
        {
            var data = await _context.Set<TEntity>().FindAsync(entityId);
            return data.DeletedDate != null ? null : data;
        }

        public virtual async Task<TEntity> LogicalDelete(string id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return entity;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //Is pending to change IEnumerable isn't best choise
        public IQueryable<TEntity> FindWithSpecificationPattern(ISpecification<TEntity> specification = null)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
        }
    }
}
