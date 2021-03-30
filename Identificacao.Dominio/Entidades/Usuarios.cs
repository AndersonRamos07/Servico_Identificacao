namespace Identificacao.Dominio.Entidades
{
    public class Usuarios : Entidade
    {
        public Usuarios(string nome, string ultimoNome, string email, string senha, string celular, string status, string supervisor, int idEmpresa)
        {
            Nome = nome;
            UltimoNome = ultimoNome;
            Email = email;
            Senha = senha;
            Celular = celular;
            Status = status;
            Supervisor = supervisor;
            IdEmpresa = idEmpresa;
        }

        public string Nome { get; private set; }
        public string UltimoNome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Celular { get; private set; }
        public string Status { get; private set; }
        public string Supervisor { get; private set; }
        public int IdEmpresa { get; private set; }

        public bool ValidaçãoIdEmpresa(int idEmpresa)
        {
            if (IdEmpresa == idEmpresa)
                return true;

            return false;
        }

        public void AtribuiIdEmpresa(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
        }

        public bool VerificaPermissaoUsuario()
        {
            if (Supervisor == "S")
                return true;

            return false;
        }

        public void Editar_dados_usuarios(string nome, string ultimoNome, string celular, string status)
        {
            Nome = nome;
            UltimoNome = ultimoNome;
            Celular = celular;
            Status = status;
        }

        public void Editar_senha_do_usuario(string senha)
        {
            Senha = senha;
        }

        public bool Verificar_senha_usuario(string senha)
        {
            if (Senha == senha)
                return true;

            return false;
        }
    }
}
