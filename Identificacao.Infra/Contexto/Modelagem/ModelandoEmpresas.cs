using Identificacao.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identificacao.Infra.Contexto.Modelagem
{
    public class ModelandoEmpresas : IEntityTypeConfiguration<Empresas>
    {
        public void Configure(EntityTypeBuilder<Empresas> builder)
        {
            builder.ToTable("EMPRESAS");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Cnpj).HasColumnName("CNPJ").HasColumnType("VARCHAR(18)").HasMaxLength(18);
            builder.Property(x => x.CodigoRegimeTributavel).HasColumnName("CODIGO_REGIME_TRIBUTAVEL").HasColumnType("INT").HasDefaultValue(0);
            builder.Property(x => x.RazaoSocial).HasColumnName("RAZAO_SOCIAL").HasColumnType("VARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.Cep).HasColumnName("CEP").HasColumnType("VARCHAR(9)").HasMaxLength(9);
            builder.Property(x => x.Logradouro).HasColumnName("LOGRADOURO").HasColumnType("VARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.Numero).HasColumnName("NUMERO").HasColumnType("VARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.Complemento).HasColumnName("COMPLEMENTO").HasColumnType("VARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.Bairro).HasColumnName("BAIRRO").HasColumnType("VARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.CodCidade).HasColumnName("COD_CIDADE").HasColumnType("VARCHAR(7)").HasMaxLength(7);
            builder.Property(x => x.Cidade).HasColumnName("CIDADE").HasColumnType("VARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.Uf).HasColumnName("UF").HasColumnType("VARCHAR(2)").HasMaxLength(2);
            builder.Property(x => x.AccessToken).HasColumnName("ACCESS_TOKEN").HasColumnType("CHAR(36)").HasMaxLength(36);
            builder.Property(x => x.InscricaoEstadual).HasColumnName("INSCRICAO_ESTADUAL").HasColumnType("VARCHAR(15)").HasMaxLength(15);
        }
    }
}
