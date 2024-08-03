using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repository.Repositories;

public class ProfissionalMedicoRepository<TContext> : BaseRepository<TContext, ProfissionalMedico>, IProfissionalMedicoRepository<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private DbSet<ProfissionalMedico> _dbSet => ((HealthMedContext)UnitOfWork).Set<ProfissionalMedico>();

    public ProfissionalMedicoRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

    public ProfissionalMedico GetByEmail(string email)
    {
        return  _dbSet.AsNoTracking().FirstOrDefault(p => p.Email == email);
    }
}