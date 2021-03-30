using Flunt.Notifications;
using Flunt.Validations;
using Identificacao.Dominio.Comandos.Contratos;

namespace Identificacao.Dominio.Comandos.Usuarios
{
    public class ComandoCriarUsuarioSuper : Notifiable, ICommand
    {
        public ComandoCriarUsuarioSuper() { }

        public ComandoCriarUsuarioSuper(string nome, string ultimoNome, string email, string senha, string celular)
        {
            Nome = nome;
            UltimoNome = ultimoNome;
            Email = email;
            Senha = senha;
            Celular = celular;
        }

        public string Nome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Celular { get; set; }

        private void CamposObrigatorios()
        {
            AddNotifications(new Contract()
                            .Requires()
                            .IsNotNullOrEmpty(Nome, "Nome", "É necessário informar o nome")
                            .IsNotNullOrEmpty(UltimoNome, "Ultimo Nome", "É necessário informar o ultimo nome")
                            .IsEmailOrEmpty(Email, "Email", "É necessario informar um email valido")
                            .IsNotNullOrEmpty(Senha, "Senha", "É necessário informar uma nova senha")
                            .IsNotNullOrEmpty(Celular, "Celular", "É necessário informar o celular"));
        }

        private void ConfiguracaoDosTamanhosDosCamposRecebidos()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLengthIfNotNullOrEmpty(UltimoNome, 2, "Ultimo Nome", "O Ultimo Nome informado deve conter no minimo de 2 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(UltimoNome, 60, "Ultimo Nome", "O Ultimo Nome informado deve conter no maximo de 60 caracteres")
                .HasMinLengthIfNotNullOrEmpty(Nome, 2, "Nome", "O Nome informado deve conter no minimo de 2 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(Nome, 60, "Nome", "O Nome informado deve conter no maximo de 60 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(Email, 150, "Email", "O email informado deve ser no maximo de 150 caracteres")
                .HasMinLengthIfNotNullOrEmpty(Senha, 4, "Senha", "A senha cadastrada deve ser de no minimo de 4 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(Senha, 60, "Senha", "A senha cadastrada deve ser de no maximo de 60 caracteres")
                .HasExactLengthIfNotNullOrEmpty(Celular, 14, "Celular", "O celular informado deve conter no maximo de 14 caracteres"));
        }

        public void Validate()
        {
            CamposObrigatorios();
            ConfiguracaoDosTamanhosDosCamposRecebidos();
        }
    }
}
