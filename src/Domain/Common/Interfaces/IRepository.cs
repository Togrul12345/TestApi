using Domain.Common.Entities;
using System.Linq.Expressions;

namespace Domain.Common.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKey> : IDisposable where TEntity : BaseEntity<TPrimaryKey> 
    {
        #region GetMethods 
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetFirstAsync(TPrimaryKey id);
        Task<TEntity> GetFirstIncludingAsync(TPrimaryKey id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(TPrimaryKey id);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        #region GetListAsync
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
        Task<TEntity> GetSingleIncludingAsync(TPrimaryKey id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Find Methods
        ValueTask<TEntity> Find(TPrimaryKey id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        #endregion

        #region Any,Count Methods
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Add , Update , Delete , Commit Methods
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task CommitAsync(CancellationToken cancellationToken);
        #endregion
    }
}
