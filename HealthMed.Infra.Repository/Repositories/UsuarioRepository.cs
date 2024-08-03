using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repository.Repositories;

public class UsuarioRepository<TContext> : BaseRepository<TContext, Usuario>, IUsuarioRepository<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private DbSet<Usuario> _dbSet => ((HealthMedContext)UnitOfWork).Set<Usuario>();

    public UsuarioRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

    Usuario IUsuarioRepository<TContext>.GetByEmail(string email)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(p => p.Email == email);
    }
}