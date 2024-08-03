using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Domain.Interfaces.Services;

namespace HealthMed.Domain.Services;

public class UsuarioService<TContext> : BaseService<TContext, Usuario>, IUsuarioService<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IUsuarioRepository<TContext> _repository;

    public UsuarioService(
                            IUsuarioRepository<TContext> repository,
                            IUnitOfWork<TContext> unitOfWork) : base(repository, unitOfWork)
    {
        _repository = repository;
    }

    public Usuario GetByEmail(string email)
    {
        return _repository.GetByEmail(email);
    }
}