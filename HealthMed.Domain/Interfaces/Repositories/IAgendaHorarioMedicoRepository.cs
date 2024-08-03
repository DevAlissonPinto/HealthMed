using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Repositories;

public interface IAgendaHorarioMedicoRepository<TContext> : IBaseRepository<TContext, AgendaHorarioMedico>
    where TContext : IUnitOfWork<TContext>
{
    Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId);

}