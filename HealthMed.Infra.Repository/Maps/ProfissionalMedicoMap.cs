using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infra.Repository.Maps
{
    public class ProfissionalMedicoMap : IEntityTypeConfiguration<ProfissionalMedico>
    {
        public void Configure(EntityTypeBuilder<ProfissionalMedico> builder)
        {
            builder.ToTable("ProfissionalMedico", "dbo");
            builder.Property(e => e.NumeroCRM).IsRequired().HasColumnName("NumeroCRM").HasColumnType("nvarchar(20)");
            builder.HasMany(e => e.HorariosMedico).WithOne(e => e.Medico).HasForeignKey(e => e.MedicoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}