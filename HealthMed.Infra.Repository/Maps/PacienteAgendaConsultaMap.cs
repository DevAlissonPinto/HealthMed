using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infra.Repository.Maps
{
    public class PacienteAgendaConsultaMap : IEntityTypeConfiguration<PacienteAgendaConsulta>
    {
        public void Configure(EntityTypeBuilder<PacienteAgendaConsulta> builder)
        {
            builder.ToTable("PacienteAgendaConsulta", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("int");

            builder.Property(e => e.MedicoId).HasColumnName("MedicoId").HasColumnType("int");
            builder.Property(e => e.PacienteId).HasColumnName("PacienteId").HasColumnType("int");
            builder.Property(e => e.DataInclusao).HasColumnName("DataInclusao").HasColumnType("datetime");

            builder.HasOne(e => e.Horario)
                   .WithMany() // Assumindo que não há uma coleção de PacienteAgendaConsulta em AgendaHorarioMedico
                   .HasForeignKey("HorarioId") // Assumindo que há uma chave estrangeira "HorarioId"
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Medico)
                   .WithMany() 
                   .HasForeignKey(e => e.MedicoId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}