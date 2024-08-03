using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Domain.Services;

public class PacienteAgendaConsultaService<TContext> : BaseService<TContext, PacienteAgendaConsulta>, IPacienteAgendaConsultaService<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IPacienteAgendaConsultaRepository<TContext> _repository;

    public PacienteAgendaConsultaService(
                            IPacienteAgendaConsultaRepository<TContext> repository,
                            IUnitOfWork<TContext> unitOfWork) : base(repository, unitOfWork)
    {
        _repository = repository;
    }

}