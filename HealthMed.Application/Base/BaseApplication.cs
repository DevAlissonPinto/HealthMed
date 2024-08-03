using HealthMed.Domain.Entities.Base;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application.Base;
using HealthMed.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace HealthMed.Application.Base;

public class BaseApplication<TContext, TEntity> : IBaseApplication<TContext, TEntity>
   //where TEntity : EntityBase
   where TContext : IUnitOfWork<TContext>
{
    protected readonly IBaseService<TContext, TEntity> _service;
    protected readonly IUnitOfWork<TContext> _unitOfWork;

    public BaseApplication(IUnitOfWork<TContext> context, IBaseService<TContext, TEntity> service)
    {
        this._service = service;
        _unitOfWork = context;
    }


    public virtual TEntity Save(TEntity entity)
    {
        _service.Save(entity);
        _unitOfWork.Commit();

        return entity;
    }
    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        await _service.SaveAsync(entity);
        _unitOfWork.Commit();

        return entity;
    }
    public virtual TEntity Update(TEntity entity)
    {
        _service.Update(entity);
        _unitOfWork.Commit();

        return entity;
    }
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await _service.UpdateAsync(entity);
        _unitOfWork.Commit();

        return entity;
    }
    public virtual void Delete(int id)
    {
        _service.Delete(id);
        _unitOfWork.Commit();
    }
    public virtual async Task DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        _unitOfWork.Commit();
    }
    public virtual TEntity Get(int id)
    {
        var entidade = _service.Get(id);
        return entidade;
    }

    public virtual TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return _service.Get(id, includeProperties);
    }
    public virtual async Task<TEntity> GetAsync(int id)
    {
        return await _service.GetAsync(id);
    }

    public virtual async Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await _service.GetAsync(id, includeProperties);
    }

    public virtual async Task<TEntity> GetNoTrackingAsync(int id)
    {
        return await _service.GetNoTrackingAsync(id);
    }
    public virtual async Task<TEntity> GetNoTrackingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await _service.GetNoTrackingAsync(id, includeProperties);
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
    {
        return _service.GetAll(predicate);
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return _service.GetAll(predicate, includeProperties);
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        return await _service.GetAllAsync(predicate);
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await _service.GetAllAsync(predicate, includeProperties);
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await _service.GetAllNoTrackingAsync(predicate, includeProperties);
    }
    public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true)
    {
        return _service.GetPaginated(filter, start, limit);
    }
    public virtual async Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true)
    {
        return await _service.GetPaginatedAsync(filter, start, limit, orderByDescending);
    }
    public virtual async Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, string orderByProperty = "Id", bool orderByDescending = true)
    {
        return await _service.GetPaginatedAsync(filter, start, limit, orderByProperty, orderByDescending);
    }
    public virtual void ClearTransaction() => _service.ClearTransaction();
    public virtual void BeginTransaction() => _service.BeginTransaction();
    public virtual void CommitTransaction() => _service.CommitTransaction();
    public virtual void RollbackTransaction() => _service.RollbackTransaction();
}
