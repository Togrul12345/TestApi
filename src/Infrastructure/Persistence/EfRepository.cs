using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class EfRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        private readonly BaseDbContext _dbContext;
        private DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public EfRepository(BaseDbContext dbContext) => _dbContext = dbContext;

        #region GetMethods 
        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = Table;
            return query;
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            BindIncludeProperties(query, includeProperties);
            includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public Task<List<TEntity>> GetAllListIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToListAsync();
        }

        public Task<TEntity> GetFirstAsync(TPrimaryKey id)
        {
            return GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id))!;
        }

        public Task<TEntity> GetFirstIncludingAsync(TPrimaryKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.FirstOrDefaultAsync(CreateEqualityExpressionForId(id))!;
        }

        public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefaultAsync(predicate)!;
        }

        public Task<TEntity> GetFirstIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.FirstOrDefaultAsync(predicate)!;
        }

        public Task<TEntity> GetSingleAsync(TPrimaryKey id)
        {
            return GetAll().SingleOrDefaultAsync(CreateEqualityExpressionForId(id))!;
        }

        public Task<TEntity> GetSingleIncludingAsync(TPrimaryKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.SingleOrDefaultAsync(CreateEqualityExpressionForId(id))!;
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleOrDefaultAsync(predicate)!;
        }

        public Task<TEntity> GetSingleIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.SingleOrDefaultAsync(predicate)!;
        }
        public Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.AllAsync(predicate);
        }
        #endregion

        #region Find Methods

        public ValueTask<TEntity> Find(TPrimaryKey id)
        {
            return Table.FindAsync(id)!;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.Where(predicate);
        }

        #endregion

        #region Any,Count Methods
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.AnyAsync(predicate);
        }
        public async Task<int> CountAsync()
        {
            return await Table.CountAsync();
        }
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.CountAsync(predicate);
        }
        #endregion

        #region Add , Update , Delete , Commit Methods
        public async Task AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
             _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = Table.Where(predicate);

            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
        }
        public async Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Private Methods
        private static void BindIncludeProperties(IQueryable<TEntity> query, IEnumerable<Expression<Func<TEntity, object>>> includeProperties)
        {
            includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        #endregion
        public void Dispose() => _dbContext?.Dispose();

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToListAsync();
        }
    }
}
