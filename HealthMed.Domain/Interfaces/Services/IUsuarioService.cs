using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Services;

public interface IUsuarioService<TContext> : IBaseService<TContext, Usuario> where TContext : IUnitOfWork<TContext>
{
    Usuario GetByEmail(string email);
}