using Flunt.Notifications;
using Flunt.Validations;
using Identificacao.Dominio.Comandos.Contratos;
using System;

namespace Identificacao.Dominio.Comandos.Usuarios
{
    public class ComandoEditarSenhaUsuario : Notifiable, ICommand
    {
        public ComandoEditarSenhaUsuario() { }
        public ComandoEditarSenhaUsuario(int id, string senhaAntiga, string senhaNova)
        {
            Id = id;
            SenhaAntiga = senhaAntiga;
            SenhaNova = senhaNova;
        }

        public int Id { get; set; }
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }
        public Guid AccessToken { get; private set; }

        private void CamposObrigatorios()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(AccessToken, Guid.Empty, "AccessToken", "É necessário informar um accessToken valido")
                .IsNotNullOrEmpty(SenhaAntiga, "SenhaAntiga", "É obrigatório informar uma nova senha")
                .IsNotNullOrEmpty(SenhaNova, "SenhaNova", "É obrigatório informar uma nova senha")
                .IsGreaterThan(Id,0,"Id","Deve-se informar um valor de Id válido"));
        }

        private void ConfiguracaoDosTamanhosDosCamposRecebidos()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLengthIfNotNullOrEmpty(SenhaAntiga, 4, "Senha", "Este campo deve ser de no minimo de 4 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(SenhaAntiga, 60, "Senha", "Este campo deve ser de no maximo de 60 caracteres")
                .HasMinLengthIfNotNullOrEmpty(SenhaNova, 4, "Senha", "Este campo deve ser de no minimo de 4 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(SenhaNova, 60, "Senha", "Este campo deve ser de no maximo de 60 caracteres"));
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
