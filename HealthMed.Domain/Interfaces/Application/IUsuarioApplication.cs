using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces.Application.Base;

namespace HealthMed.Domain.Interfaces.Application;

public interface IUsuarioApplication<TContext> : IBaseApplication<TContext, Usuario>
where TContext : IUnitOfWork<TContext>
{
    Usuario GetByEmail(string email);
}