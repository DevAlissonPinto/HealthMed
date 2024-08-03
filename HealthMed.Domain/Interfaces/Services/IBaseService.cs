using HealthMed.Domain.Entities.Base;
using System.Linq.Expressions;

namespace HealthMed.Domain.Interfaces.Services;

public interface IBaseService<TContext, TEntity>
 where TContext : IUnitOfWork<TContext>
{
    TEntity Save(TEntity entity);
    Task<TEntity> SaveAsync(TEntity entidade);
    TEntity Update(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    void Delete(int chave);
    Task DeleteAsync(int id);
    TEntity Get(int id);
    TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity> GetAsync(int id);
    Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity> GetNoTrackingAsync(int id);
    Task<TEntity> GetNoTrackingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps);
    Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps);
    IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true);
    Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true);
    Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, string orderByProperty = "Id", bool orderByDescending = true);
    void ClearTransaction();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
