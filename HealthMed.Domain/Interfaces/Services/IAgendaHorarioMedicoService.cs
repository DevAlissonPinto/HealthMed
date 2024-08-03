using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Services;

public interface IAgendaHorarioMedicoService<TContext> : IBaseService<TContext, AgendaHorarioMedico> where TContext : IUnitOfWork<TContext>
{
    Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId);
}