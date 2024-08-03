using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace HealthMed.Domain.Services;

public class ProfissionalMedicoService<TContext> : BaseService<TContext, ProfissionalMedico>, IProfissionalMedicoService<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IProfissionalMedicoRepository<TContext> _repository;

    public ProfissionalMedicoService(
                            IProfissionalMedicoRepository<TContext> repository,
                            IUnitOfWork<TContext> unitOfWork) : base(repository, unitOfWork)
    {
        _repository = repository;
    }

    public override async Task<IEnumerable<ProfissionalMedico>> GetAllAsync(Expression<Func<ProfissionalMedico, bool>> predicate = null)
    {
        var profissionais = await base.GetAllAsync(predicate);
        
        if (profissionais is null || !profissionais.Any())
        {
            throw new Exception("Não tem profissional disponível");
        }

        return profissionais;
    }

    public ProfissionalMedico GetByEmail(string email)
    {
         return _repository.GetByEmail(email);
    }
}