using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infra.Repository.Maps
{
    public class AgendaHorarioMedicoMap : IEntityTypeConfiguration<AgendaHorarioMedico>
    {
        public void Configure(EntityTypeBuilder<AgendaHorarioMedico> builder)
        {
            builder.ToTable("AgendaHorarioMedico", "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int");

            builder.Property(e => e.DataHora)
                .IsRequired()
                .HasColumnName("DataHora")
                .HasColumnType("datetime");

            builder.Property(e => e.Disponivel)
                .IsRequired()
                .HasColumnName("Disponivel")
                .HasColumnType("bit");

            builder.Property(e => e.MedicoId)
                .IsRequired()
                .HasColumnName("MedicoId")
                .HasColumnType("int");

            builder.HasOne(e => e.Medico)
                .WithMany()
                .HasForeignKey(e => e.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Se você precisar de mais configurações, pode adicioná-las aqui.
        }
    }
}