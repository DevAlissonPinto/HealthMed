using HealthMed.Application;
using HealthMed.Application.Base;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Application.Base;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Domain.Services;
using HealthMed.Infra.Repository.Context;
using HealthMed.Infra.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HealthMed.Domain.Interfaces.Services;

namespace HealthMed.Infra.Ioc;

public static class HealthMedIoc
{
    public static void Initialize(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<HealthMedContext>(options => options.UseSqlServer(configuration.GetConnectionString("HealthMedConnection")));
        services.AddScoped<IUnitOfWork<HealthMedContext>, HealthMedContext>();

        services.AddScoped(typeof(IBaseApplication<,>), typeof(BaseApplication<,>));
        services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
        services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

        services.AddScoped(typeof(IAgendaHorarioMedicoApplication<>), typeof(AgendaHorarioMedicoApplication<>));
        services.AddScoped(typeof(IAgendaHorarioMedicoService<>), typeof(AgendaHorarioMedicoService<>));
        services.AddScoped(typeof(IAgendaHorarioMedicoRepository<>), typeof(AgendaHorarioMedicoRepository<>));

        services.AddScoped(typeof(IPacienteAgendaConsultaApplication<>), typeof(PacienteAgendaConsultaApplication<>));
        services.AddScoped(typeof(IPacienteAgendaConsultaService<>), typeof(PacienteAgendaConsultaService<>));
        services.AddScoped(typeof(IPacienteAgendaConsultaRepository<>), typeof(PacienteAgendaConsultaRepository<>));

        services.AddScoped(typeof(IProfissionalMedicoApplication<>), typeof(ProfissionalMedicoApplication<>));
        services.AddScoped(typeof(IProfissionalMedicoService<>), typeof(ProfissionalMedicoService<>));
        services.AddScoped(typeof(IProfissionalMedicoRepository<>), typeof(ProfissionalMedicoRepository<>));


        services.AddScoped(typeof(IUsuarioApplication<>), typeof(UsuarioApplication<>));
        services.AddScoped(typeof(IUsuarioService<>), typeof(UsuarioService<>));
        services.AddScoped(typeof(IUsuarioRepository<>), typeof(UsuarioRepository<>));

    }

}
