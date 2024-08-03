using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repository.Repositories;

public class PacienteAgendaConsultaRepository<TContext> : BaseRepository<TContext, PacienteAgendaConsulta>, IPacienteAgendaConsultaRepository<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private DbSet<PacienteAgendaConsulta> _dbSet => ((HealthMedContext)UnitOfWork).Set<PacienteAgendaConsulta>();

    public PacienteAgendaConsultaRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

    //public async Task<IEnumerable<AgendaHorarioMedico>> GetByMedicoIdAsync(int medicoId)
    //{
       
        //return await _context.AgendaHorarioMedico
        //                     .Where(ahm => ahm.MedicoId == medicoId)
        //                     .ToListAsync();
    //}
}