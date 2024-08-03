using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces.Application.Base;

namespace HealthMed.Domain.Interfaces.Application;

public interface IAgendaHorarioMedicoApplication<TContext> : IBaseApplication<TContext, AgendaHorarioMedico>
where TContext : IUnitOfWork<TContext>
{
    Task SaveRangeAsync(IEnumerable<AgendaHorarioMedico> horarios);

    Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId);
}