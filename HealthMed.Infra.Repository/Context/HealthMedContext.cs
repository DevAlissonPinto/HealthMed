using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Infra.Repository.Maps;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repository.Context;

public class HealthMedContext : IdentityDbContext<ApplicationUser>, IUnitOfWork<HealthMedContext>
{
    public HealthMedContext(DbContextOptions<HealthMedContext> options) : base(options) { }

    public int Commit() => this.SaveChanges();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfiguration(new AlmoxarifadoMap());
        //modelBuilder.ApplyConfiguration(new MaterialMap());

        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new ProfissionalMedicoMap());
        modelBuilder.ApplyConfiguration(new PacienteAgendaConsultaMap());
        modelBuilder.ApplyConfiguration(new AgendaHorarioMedicoMap());


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthMedContext).Assembly);

    }
}
