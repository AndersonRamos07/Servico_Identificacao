using Identificacao.Dominio.Entidades;
using Identificacao.Infra.Contexto.Modelagem;
using Microsoft.EntityFrameworkCore;

namespace Identificacao.Infra.Contexto
{
    public partial class DadosContexto : DbContext
    {
        public DadosContexto() { }

        public DadosContexto(DbContextOptions<DadosContexto> options) : base(options) { }

        public DbSet<Usuarios> usuarios { get; set; }
        public DbSet<Empresas> Empresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ModelandoUsuario());
            modelBuilder.ApplyConfiguration(new ModelandoEmpresas());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
