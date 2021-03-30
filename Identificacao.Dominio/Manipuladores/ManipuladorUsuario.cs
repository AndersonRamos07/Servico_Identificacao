using Flunt.Notifications;
using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Comandos.Contratos;
using Identificacao.Dominio.Comandos.Usuarios;
using Identificacao.Dominio.Entidades;
using Identificacao.Dominio.Manipuladores.Contratos;
using Identificacao.Dominio.Repositorios;
using System.Collections.Generic;

namespace Identificacao.Dominio.Manipuladores
{
    public class ManipuladorUsuario :
        Notifiable,
        IHandler<ComandoCriarUsuario>,
        IHandler<ComandoLogarNoSistema>,
        IHandler<ComandoEditarUsuario>,
        IHandler<ComandoCriarUsuarioSuper>,
        IHandler<ComandoEditarSenhaUsuario>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ManipuladorUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public ICommandResult handle(ComandoCriarUsuario command)
        {
            // 1 - Validação Rapida dos campos
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", command.Notifications);

            // 2 - Verificar se o email ja possui na base de dados
            var verificacao = _repositorioUsuario.Obter_usuario_email(command.Email);
            if (verificacao != null)
            {
                AddNotification(verificacao.Email, "Email ja esta cadastrado em nossa base");
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", Notifications);
            };

            // 3 - Recupera o id da empresa
            var idEmpresa = _repositorioUsuario.ObterEmpresaPorAccessToken(command.AccessToken);

            // 4 - Verifica se o id da empresa é maior que 0 se for igual a 0 retorna mensagem de erro
            if (idEmpresa == 0)
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario, pois o accessToken informado não foi localizado", null);

            // 5 - Criar entidade
            var usuario = new Usuarios(
                command.Nome,
                command.UltimoNome,
                command.Email,
                command.Senha,
                command.Celular,
                "A",
                "N",
                idEmpresa
                );

            // 6 - Salva a entidade no repositorio
            _repositorioUsuario.Criar_usuario(usuario);

            // 7 - Retornar dados na tela
            var data = new Dictionary<string, string>();
            data.Add("id", usuario.Id.ToString());
            data.Add("nome", usuario.Nome);


            // 8 - Retornar dados na tela
            return new CommandResultGeneric(true, "Usuario cadastrado com sucesso", data);
        }

        public ICommandResult handle(ComandoLogarNoSistema command)
        {
            // 1 - Validação Rapida dos campos
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", command.Notifications);

            // 2 - recuperar dados
            var verificacao = _repositorioUsuario.Obter_usuario_email(command.Email);

            // 3 - Verifica se o email esta cadastrado na base de dados
            if (verificacao == null)
            {
                AddNotification(command.Email, "Este email não esta cadastrado!");
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", Notifications);
            };

            // 4 - Verifica senhas 
            var resultado = verificacao.Verificar_senha_usuario(command.Senha);
            if (!resultado)
                return new CommandResultGeneric(false, "Usuario ou senha estão incorretos", null);

            // Consulta empresa
            var consulta_empresa = _repositorioUsuario.ObterAccessToken(verificacao.IdEmpresa);

            // 5 - Retornar dados na tela
            var data = new Dictionary<string, string>();
            data.Add("id", verificacao.Id.ToString());
            data.Add("nome", verificacao.Nome);
            data.Add("accessToken", consulta_empresa.ToString());

            return new CommandResultGeneric(true, "Login ok", data);
        }

        public ICommandResult handle(ComandoEditarUsuario command)
        {
            // 1 - Validação Rapida dos campos
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", command.Notifications);

            // 2 - Recupera o id da empresa
            var idEmpresa = _repositorioUsuario.ObterEmpresaPorAccessToken(command.AccessToken);

            // 3 - Verifica se o id da empresa é maior que 0 se for igual a 0 retorna mensagem de erro
            if (idEmpresa == 0)
                return new CommandResultGeneric(false, "Não foi possível atualizar os dados do usuario, pois o accessToken informado não foi localizado", null);

            // 4 - Recupera o usuario atraves do email de cadastro
            var usuario = _repositorioUsuario.Obter_usuario_email(command.Email);

            // 5 - Verifica se foi localizado algum email no repositorio
            if (usuario == null)
                return new CommandResultGeneric(false, "Não foi possível atualizar os dados do usuario, pois não localizamos o usuario", null);

            // 6 - Valida se o status esta com as informações corretas
            if (command.Status != "A" && command.Status != "I")
                return new CommandResultGeneric(false, "Não foi possível atualizar os dados do usuario, pois o status foi informado errado deve ser informado A de Ativo ou I de Inativo", null);

            // 7 - Edita os dados do usuario
            usuario.Editar_dados_usuarios(command.Nome,
                                        command.UltimoNome,
                                        command.Celular,
                                        command.Status);

            // 8 - Atualizo os dados no repositorio
            _repositorioUsuario.Editar_usuario(usuario);

            // 8 - Retornar dados na tela
            var data = new Dictionary<string, string>();
            data.Add("id", usuario.Id.ToString());
            data.Add("nome", usuario.Nome);


            // 9 - Retornar dados na tela
            return new CommandResultGeneric(true, "Usuario atualizado com sucesso", data);


        }

        public ICommandResult handle(ComandoCriarUsuarioSuper command)
        {
            // 1 - Validação Rapida dos campos
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", command.Notifications);

            // 2 - Verificar se o email ja possui na base de dados
            var verificacao = _repositorioUsuario.Obter_usuario_email(command.Email);
            if (verificacao != null)
            {
                AddNotification(verificacao.Email, "Email ja esta cadastrado em nossa base");
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", Notifications);
            };

            // 3 - Criar entidade
            var usuario = new Usuarios(
                command.Nome,
                command.UltimoNome,
                command.Email,
                command.Senha,
                command.Celular,
                "A",
                "S",
                0
                );

            // 4 - Salva a entidade no repositorio
            _repositorioUsuario.Criar_usuario(usuario);

            // 5 - Cria um dicionario para retornar alguns dados especificos
            var data = new Dictionary<string, string>();
            data.Add("id", usuario.Id.ToString());
            data.Add("nome", usuario.Nome);


            // 6 - Retornar mensagem de sucesso e os dados do dicionario
            return new CommandResultGeneric(true, "Usuario cadastrado com sucesso", data);
        }

        public ICommandResult handle(ComandoEditarSenhaUsuario command)
        {
            command.Validate();
            if (command.Invalid)
                return new CommandResultGeneric(false, "Não foi possível cadastrar o usuario", command.Notifications);

            // 2 - Recupera o id da empresa
            var idEmpresa = _repositorioUsuario.ObterEmpresaPorAccessToken(command.AccessToken);

            // 3 - Verifica se o id da empresa é maior que 0 se for igual a 0 retorna mensagem de erro
            if (idEmpresa == 0)
                return RetornoDeUsuarioEhTokenNaoConferem();

            // 4 - Recupera os dados do usuario através do id informado pelo comando recebido
            var usuario = _repositorioUsuario.Obter_usuario_id(command.Id);

            // 5 - Valida se retornou algum usuario
            if(usuario == null)
                return RetornoDeUsuarioEhTokenNaoConferem();

            // 6 - Valida se o usuario pertence a empresa informada pelo accessToken
            if (!usuario.ValidaçãoIdEmpresa(idEmpresa))
                return RetornoDeUsuarioEhTokenNaoConferem();

            // 7 - Verifica se a senha antiga confere com a senha que consta no repositorio
            if(!usuario.Verificar_senha_usuario(command.SenhaAntiga))
                return RetornoDeUsuarioEhTokenNaoConferem();

            // 7 - Edita a senha do usuario
            usuario.Editar_senha_do_usuario(command.SenhaNova);

            // 8 - Salva os novos dados no repositorio de usuarios
            _repositorioUsuario.Editar_usuario(usuario);

            // 9 - Retorna sucesso da execução do comando
            return new CommandResultGeneric(true, "A senha do usuario foi alterada com sucesso",null);

        }

        private ICommandResult RetornoDeUsuarioEhTokenNaoConferem()
        {
            return new CommandResultGeneric(false, "Não foi possível atualizar a senha do usuario, pois o usuario ou token informado não foi localizado", null);
        }
    }
}
