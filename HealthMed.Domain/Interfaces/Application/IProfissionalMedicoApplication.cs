using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces.Application.Base;

namespace HealthMed.Domain.Interfaces.Application;

public interface IProfissionalMedicoApplication<TContext> : IBaseApplication<TContext, ProfissionalMedico>
where TContext : IUnitOfWork<TContext>
{
    ProfissionalMedico GetByEmail(string email);
}