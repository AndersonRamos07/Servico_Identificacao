using Flunt.Notifications;
using Flunt.Validations;
using Identificacao.Dominio.Comandos.Contratos;
using System;

namespace Identificacao.Dominio.Comandos.Usuarios
{
    public class ComandoEditarUsuario : Notifiable, ICommand
    {
        public ComandoEditarUsuario() { }

        public ComandoEditarUsuario(int id, string nome, string ultimoNome, string email, string celular, string status)
        {
            Id = id;
            Nome = nome;
            UltimoNome = ultimoNome;
            Email = email;
            Celular = celular;
            Status = status;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Status { get; set; }
        public Guid AccessToken { get; private set; }

        private void CamposObrigatorios()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Nome, "Nome", "É necessário informar o nome")
                .IsNotNullOrEmpty(UltimoNome, "Ultimo Nome", "É necessário informar o ultimo nome")
                .IsEmailOrEmpty(Email, "Email", "É necessario informar um email valido")
                .IsNotNullOrEmpty(Status, "Status", "É necessário informar o status")
                .IsNotNullOrEmpty(Celular, "Celular", "É necessário informar o celular")
                .AreNotEquals(AccessToken, Guid.Empty, "AccessToken", "É necessário informar um accessToken valido")
                .IsGreaterThan(Id, 0, "Id", "Deve-se informar um valor de Id válido"));
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
                .HasExactLengthIfNotNullOrEmpty(Celular, 14, "Celular", "O celular informado deve conter no maximo de 14 caracteres"));
        }

        public void InformarOAccessToken(Guid accessToken)
        {
            AccessToken = accessToken;
        }

        public void Validate()
        {
            CamposObrigatorios();
            ConfiguracaoDosTamanhosDosCamposRecebidos();
        }
    }
}
