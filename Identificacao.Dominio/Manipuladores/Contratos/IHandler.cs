using Identificacao.Dominio.Comandos.Contratos;

namespace Identificacao.Dominio.Manipuladores.Contratos
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult handle(T command);
    }
}
