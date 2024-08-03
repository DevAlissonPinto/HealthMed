using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces.Services;

public interface IPacienteAgendaConsultaService<TContext> : IBaseService<TContext, PacienteAgendaConsulta> where TContext : IUnitOfWork<TContext>
{
}