using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repository.Repositories;

public class AgendaHorarioMedicoRepository<TContext> : BaseRepository<TContext, AgendaHorarioMedico>, IAgendaHorarioMedicoRepository<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private DbSet<AgendaHorarioMedico> _dbSet => ((HealthMedContext)UnitOfWork).Set<AgendaHorarioMedico>();

    public AgendaHorarioMedicoRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

    public async Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId)
    {
        return await _dbSet.AsNoTracking().Where(x => x.MedicoId == medicoId).ToListAsync();
    }
}