using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces.Application.Base;
using HealthMed.Domain.Interfaces.Repositories;

namespace HealthMed.Domain.Interfaces.Application;


public interface IPacienteAgendaConsultaApplication<TContext> : IBaseApplication<TContext, PacienteAgendaConsulta>
where TContext : IUnitOfWork<TContext>
{
    //Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId);

}