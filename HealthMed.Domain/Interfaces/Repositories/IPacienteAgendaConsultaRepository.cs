using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Repositories;

public interface IPacienteAgendaConsultaRepository<TContext> : IBaseRepository<TContext, PacienteAgendaConsulta>
    where TContext : IUnitOfWork<TContext>
{
}