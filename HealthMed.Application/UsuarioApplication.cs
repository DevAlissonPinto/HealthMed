using HealthMed.Application.Base;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Services;

namespace HealthMed.Application;

public class UsuarioApplication<TContext> : BaseApplication<TContext, Usuario>, IUsuarioApplication<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IUsuarioService<TContext> _service;

    public UsuarioApplication(IUnitOfWork<TContext> context, IUsuarioService<TContext> service)
        : base(context, service)
    {
        _service = service;
    }

    public Usuario GetByEmail(string email)
    {
        return _service.GetByEmail(email);
    }
}