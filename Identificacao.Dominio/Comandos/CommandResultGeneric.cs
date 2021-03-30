using Identificacao.Dominio.Comandos.Contratos;

namespace Identificacao.Dominio.Comandos
{
    public class CommandResultGeneric : ICommandResult
    {
        public CommandResultGeneric() { }

        public CommandResultGeneric(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
    }
}
