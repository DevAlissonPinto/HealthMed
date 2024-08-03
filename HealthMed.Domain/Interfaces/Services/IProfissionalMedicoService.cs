using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Services;

public interface IProfissionalMedicoService<TContext> : IBaseService<TContext, ProfissionalMedico> where TContext : IUnitOfWork<TContext>
{
    ProfissionalMedico GetByEmail(string email);
}