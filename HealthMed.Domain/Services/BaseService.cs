using HealthMed.Domain.Entities.Base;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace HealthMed.Domain.Services;

public class BaseService<TContexto, TEntity> : IBaseService<TContexto, TEntity> where TContexto : IUnitOfWork<TContexto> where TEntity : EntityBase
{
    protected readonly IBaseRepository<TContexto, TEntity> _repository;

    protected readonly IUnitOfWork<TContexto> _unitOfWork;

    public BaseService(IBaseRepository<TContexto, TEntity> repository, IUnitOfWork<TContexto> unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public virtual TEntity Save(TEntity entidade)
    {
        entidade.AtualizarDataInclusao();
        entidade.Validate();
        _repository.Save(entidade);
        return entidade;
    }

    public virtual async Task<TEntity> SaveAsync(TEntity entidade)
    {
        entidade.AtualizarDataInclusao();
        entidade.Validate();
        await _repository.SaveAsync(entidade);
        return entidade;
    }

    public virtual TEntity Update(TEntity entidade)
    {
        entidade.AtualizarDataAlteracao();
        entidade.Validate();
        _repository.Update(entidade);
        return entidade;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.AtualizarDataAlteracao();
        entity.Validate();
        await _repository.UpdateAsync(entity);
        return entity;
    }

    public virtual void Delete(int chave)
    {
        TEntity val = _repository.Get(chave);
        val.Inativar();
        val.AtualizarDataAlteracao();
        val.Validate();
        _repository.Delete(val);
    }

    public virtual async Task DeleteAsync(int id)
    {
        TEntity val = await _repository.GetAsync(id);
        if (val != null)
        {
            val.Inativar();
            val.AtualizarDataAlteracao();
            val.Validate();
            await _repository.DeleteAsync(val);
        }
    }

    public virtual TEntity Get(int id)
    {
        return _repository.Get(id);
    }

    public virtual TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return _repository.Get(id, includeProperties);
    }

    public virtual async Task<TEntity> GetAsync(int id)
    {
        return await _repository.GetAsync(id);
    }

    public virtual async Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await _repository.GetAsync(id, includeProperties);
    }

    public virtual async Task<TEntity> GetNoTrackingAsync(int id)
    {
        return await _repository.GetNoTrackingAsync(id);
    }

    public virtual async Task<TEntity> GetNoTrackingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await _repository.GetNoTrackingAsync(id, includeProperties);
    }

    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
    {
        return _repository.GetAll(predicate);
    }

    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return _repository.GetAll(predicate, includeProperties);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        return await _repository.GetAllAsync(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps)
    {
        return await _repository.GetAllAsync(predicate, includeExps);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExps)
    {
        return await _repository.GetAllNoTrackingAsync(predicate, includeExps);
    }

    public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start, int limit, bool orderByDescending)
    {
        return _repository.GetPaginated(filter, start, limit, orderByDescending);
    }

    public virtual IQueryable<TEntity> GetQuaryable(Expression<Func<TEntity, bool>> predicate = null)
    {
        return _repository.GetQueryable(predicate);
    }

    public virtual async Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true)
    {
        return await _repository.GetPaginatedAsync(filter, start, limit, orderByDescending);
    }

    public virtual async Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, string orderByProperty = "Id", bool orderByDescending = true)
    {
        return await _repository.GetPaginatedAsync(filter, start, limit, orderByProperty, orderByDescending);
    }

    public virtual void ClearTransaction()
    {
        _repository.ClearTransaction();
    }

    public virtual void BeginTransaction()
    {
        _repository.BeginTransaction();
    }

    public virtual void CommitTransaction()
    {
        _repository.CommitTransaction();
    }

    public virtual void RollbackTransaction()
    {
        _repository.RollbackTransaction();
    }
}
