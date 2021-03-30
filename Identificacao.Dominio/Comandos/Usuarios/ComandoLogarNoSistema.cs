using Flunt.Notifications;
using Flunt.Validations;
using Identificacao.Dominio.Comandos.Contratos;

namespace Identificacao.Dominio.Comandos.Usuarios
{
    public class ComandoLogarNoSistema : Notifiable, ICommand
    {
        public ComandoLogarNoSistema() { }

        public ComandoLogarNoSistema(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsEmailOrEmpty(Email, "Email", "Deve-se informar um email valido!")
                .IsNotNullOrEmpty(Senha, "Senha", "O campo senha é obrigatório")
                );
        }
    }
}
