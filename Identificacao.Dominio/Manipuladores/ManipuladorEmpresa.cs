using Flunt.Notifications;
using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Comandos.Contratos;
using Identificacao.Dominio.Comandos.Empresas;
using Identificacao.Dominio.Entidades;
using Identificacao.Dominio.Manipuladores.Contratos;
using Identificacao.Dominio.Repositorios;

namespace Identificacao.Dominio.Manipuladores
{
    public class ManipuladorEmpresa :
        Notifiable,
        IHandler<ComandoCriarEmpresa>,
        IHandler<ComandoEditarEmpresa>
    {
        private readonly IRepositorioEmpresa _repositorio;

        public ManipuladorEmpresa(IRepositorioEmpresa repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult handle(ComandoEditarEmpresa command)
        {
            //Validação rapida do comando recebido
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possivel editar a empresa", command.Notifications);

            //Recupera os dados no repositorio
            var empresa = _repositorio.ObterEmpresaPorCnpj(command.Cnpj);

            // Verifica se a empresa existe no repositorio
            if (empresa == null)
                return new CommandResultGeneric(false, "O CNPJ informado não foi encontrado", null);

            //Edita os dados da entidade empresa
            empresa.EditarEmpresa(command.CodigoRegimeTributavel,
                                  command.Cnpj,
                                  command.RazaoSocial,
                                  command.Cep,
                                  command.Logradouro,
                                  command.Numero,
                                  command.Complemento,
                                  command.Bairro,
                                  command.CodCidade,
                                  command.Cidade,
                                  command.Uf,
                                  command.InscricaoEstadual);

            //Atualiza os dados no repositorio
            _repositorio.EditarEmpresa(empresa);

            //Retorna os dados de sucesso
            return new CommandResultGeneric(true, "Dados da empresa atualizado com sucesso", empresa);
        }

        public ICommandResult handle(ComandoCriarEmpresa command)
        {
            // 1 - Validação rapida do comando recebido
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possível cadastrar a empresa", command.Notifications);

            // 2 - Verifica o se o usuario tem permissão para criar empresa
            if (!_repositorio.VerificarSeUsuarioTemPermissaoSuper(command.IdUsuario))
                return new CommandResultGeneric(false, "Não foi possível cadastrar a empresa, pois o usuario não tem permissão para esta operação ou não foi localizado em nosa base", null);

            // 3 - Verifica se ja tem empresa cadastrada
            var empresa = _repositorio.ObterEmpresaPorCnpj(command.Cnpj);
            if (empresa != null)
                return new CommandResultGeneric(false, "Não foi possível cadastrar a empresa, pois o cnpj informado ja tem cadastro em nossas base", null);

            // 4 - Cria uma nova entidade de empresas
            var novaEmpresa = new Empresas(command.CodigoRegimeTributavel,
                                      command.Cnpj,
                                      command.RazaoSocial,
                                      command.Cep,
                                      command.Logradouro,
                                      command.Numero,
                                      command.Complemento,
                                      command.Bairro,
                                      command.CodCidade,
                                      command.Cidade,
                                      command.Uf,
                                      command.InscricaoEstadual);

            // 5 - Salva no repositorio a nova entidade
            _repositorio.CriarEmpresa(novaEmpresa);

            // 6 - Atualiza o repositorio de usuarios com a empresa criada
            _repositorio.AtualizaUsuarioComEmpresa(command.IdUsuario, novaEmpresa.Id);

            // 7 - Retorna os dados de sucesso
            return new CommandResultGeneric(true, "A empresa foi criada com sucesso", novaEmpresa);

        }
    }
}
