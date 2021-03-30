using Identificacao.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identificacao.Infra.Contexto.Modelagem
{
    public class ModelandoUsuario : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("USUARIOS");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_USUARIO");
            builder.Property(x => x.Nome).HasMaxLength(60).HasColumnType("varchar(60)").HasColumnName("NOME");
            builder.Property(x => x.UltimoNome).HasMaxLength(60).HasColumnType("varchar(60)").HasColumnName("ULTIMO_NOME");
            builder.Property(x => x.Senha).HasMaxLength(60).HasColumnType("varchar(60)").HasColumnName("SENHA");
            builder.Property(x => x.Email).HasMaxLength(150).HasColumnType("varchar(150)").HasColumnName("EMAIL");
            builder.Property(x => x.Celular).HasMaxLength(14).HasColumnType("varchar(14)").HasColumnName("CELULAR");
            builder.Property(x => x.Status).HasMaxLength(1).HasColumnType("char(1)").HasColumnName("STATUS");
            builder.Property(x => x.Supervisor).HasMaxLength(1).HasColumnType("char(1)").HasColumnName("SUPERVISOR");
            builder.Property(x => x.IdEmpresa).HasColumnName("ID_EMPRESA");
        }
    }
}
