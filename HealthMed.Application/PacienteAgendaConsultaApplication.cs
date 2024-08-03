using HealthMed.Application.Base;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Application.Base;
using HealthMed.Domain.Interfaces.Services;
using HealthMed.Domain.Services;

namespace HealthMed.Application;

public class PacienteAgendaConsultaApplication<TContext> : BaseApplication<TContext, PacienteAgendaConsulta>, IPacienteAgendaConsultaApplication<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IPacienteAgendaConsultaService<TContext> _service;

    public PacienteAgendaConsultaApplication(IUnitOfWork<TContext> context, IPacienteAgendaConsultaService<TContext> service)
        : base(context, service)
    {
        _service = service;
    }

}   