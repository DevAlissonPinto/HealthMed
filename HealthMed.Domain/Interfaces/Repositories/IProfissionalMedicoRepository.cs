using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Repositories;

public interface IProfissionalMedicoRepository<TContext> : IBaseRepository<TContext, ProfissionalMedico>
    where TContext : IUnitOfWork<TContext>
{
    ProfissionalMedico GetByEmail(string email);
}