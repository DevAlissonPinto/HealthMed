using HealthMed.Domain.Entities.Base;
using System.Data;
using System.Linq.Expressions;

namespace HealthMed.Domain.Interfaces.Repositories;

public interface IBaseRepository<TContext, TEntity>
   where TEntity : EntityBase
   where TContext : IUnitOfWork<TContext>
{
    TEntity Save(TEntity entity);
    Task<TEntity> SaveAsync(TEntity entity);
    void SaveRange(IEnumerable<TEntity> entities);
    Task SaveRangeAsync(IEnumerable<TEntity> entities);
    TEntity Update(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity);
    void Delete(int codigo);
    Task DeleteAsync(int codigo);
    TEntity Get(int id);
    TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    TEntity GetNoTracking(int id);
    Task<TEntity> GetAsync(int id);
    Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity> GetNoTrackingAsync(int id);
    Task<TEntity> GetNoTrackingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps);
    Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps);
    IEnumerable<TEntity> GetFilteredAndInclude(
             Expression<Func<TEntity, bool>> predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             int? limit = null,
             params Expression<Func<TEntity, object>>[] includeProperties);
    IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null);
    IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes);
    Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes);
    Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, string orderByProperty = "Id", bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes);
    void BulkInsert<T>(ICollection<T> dados, string[] colunas, string table);
    List<T> ExecuteStoredProcedureWithParam<T>(string procedureName, IDataParameter[] parameters);
    List<T> ExecuteStoredProcedure<T, F>(F filters, string procedureName);
    object ExecuteProcedureScalar(string procedureName, dynamic parameter);
    void ClearTransaction();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
