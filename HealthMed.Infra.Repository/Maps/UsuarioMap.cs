using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infra.Repository.Maps;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario", "dbo");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("int");
        builder.Property(e => e.Nome).IsRequired().HasColumnName("Nome").HasColumnType("nvarchar(100)");
        builder.Property(e => e.Email).IsRequired().HasColumnName("Email").HasColumnType("nvarchar(100)");
        builder.Property(e => e.Senha).IsRequired().HasColumnName("Senha").HasColumnType("nvarchar(100)");
        builder.Property(e => e.CPF).IsRequired().HasColumnName("CPF").HasColumnType("nvarchar(11)");

    }
}