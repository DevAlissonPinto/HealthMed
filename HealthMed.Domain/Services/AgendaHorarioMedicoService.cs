using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Domain.Services;

public class AgendaHorarioMedicoService<TContext> : BaseService<TContext, AgendaHorarioMedico>, IAgendaHorarioMedicoService<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IAgendaHorarioMedicoRepository<TContext> _repository;

    public AgendaHorarioMedicoService(
                            IAgendaHorarioMedicoRepository<TContext> repository,
                            IUnitOfWork<TContext> unitOfWork) : base(repository, unitOfWork)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId)
    {
        return await _repository.GetByMedicoIdAsync(medicoId);
    }

    public async Task SaveRangeAsync(IEnumerable<AgendaHorarioMedico> horarios)
    {
        await _repository.SaveRangeAsync(horarios);
        //await _repository.SaveChangesAsync();
    }

   
}