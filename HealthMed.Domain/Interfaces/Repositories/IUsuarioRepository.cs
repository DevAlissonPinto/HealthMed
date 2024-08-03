using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Repositories;

public interface IUsuarioRepository<TContext> : IBaseRepository<TContext, Usuario>
    where TContext : IUnitOfWork<TContext>
{
    Usuario GetByEmail(string email);
}