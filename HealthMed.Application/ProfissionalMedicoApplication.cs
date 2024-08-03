using HealthMed.Application.Base;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Services;
using HealthMed.Domain.Services;

namespace HealthMed.Application;

public class ProfissionalMedicoApplication<TContext> : BaseApplication<TContext, ProfissionalMedico>, IProfissionalMedicoApplication<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IProfissionalMedicoService<TContext> _service;

    public ProfissionalMedicoApplication(IUnitOfWork<TContext> context, IProfissionalMedicoService<TContext> service)
        : base(context, service)
    {
        _service = service;
    }

    public ProfissionalMedico GetByEmail(string email)
    {
        return _service.GetByEmail(email);
    }
}