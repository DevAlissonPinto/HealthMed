using HealthMed.Application.Base;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Services;
using HealthMed.Domain.Services;

namespace HealthMed.Application;

public class AgendaHorarioMedicoApplication<TContext> : BaseApplication<TContext, AgendaHorarioMedico>, IAgendaHorarioMedicoApplication<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IAgendaHorarioMedicoService<TContext> _service;

    public AgendaHorarioMedicoApplication(IUnitOfWork<TContext> context, IAgendaHorarioMedicoService<TContext> service)
        : base(context, service)
    {
        _service = service;
    }

    public async Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId)
    {
        return await _service.GetByMedicoIdAsync(medicoId);
    }

    public async Task SaveRangeAsync(IEnumerable<AgendaHorarioMedico> horarios)
    {
        //await _service.SaveRangeAsync(horarios);
    }

    

}   