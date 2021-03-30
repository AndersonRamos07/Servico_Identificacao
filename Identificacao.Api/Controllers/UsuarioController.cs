using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Comandos.Usuarios;
using Identificacao.Dominio.Manipuladores;
using Identificacao.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Identificacao.Api.Controllers
{
    [ApiController]
    [Route("v1/Usuarios")]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Este endpoint é reponsavel por listar os usuarios cadastrado em uma empresa
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="repositorio"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<Dictionary<string, string>> Obter_usuario(
            [FromHeader] Guid accessToken,
            [FromServices] IRepositorioUsuario repositorio
        )
        {
            return repositorio.Obter_usuario(accessToken);
        }

        /// <summary>
        /// Este endpoint é responsável por criar um usuario com perfil comum ao qual o mesmo é vinculado com uma empresa 
        /// </summary>
        /// <param name="comando"><b>Os campos no JSON deve ser preenchido das seguintes formas</b>
        /// <para> <b> *nome</b> => Este campo deve conter no minimo de 2 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *ultimoNome</b> => Este campo deve conter no minimo de 2 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *email</b> => Este campo deve conter no máximo de 150 caracteres </para>
        /// <para> <b> *senha</b> => Este campo deve conter no minimo de 4 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *celular</b> => Este campo deve ser informado (xx)xxxxx-xxxx</para>
        /// <para> <b> Observações</b> => Os Campos com * são obrigatórios para envio do comando para criar/gravar o usuario no repositorio</para>
        /// </param>
        /// <param name="AccessToken"></param>
        /// <param name="manipulador"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IActionResult Criar_usuario(
            [FromBody] ComandoCriarUsuario comando,
            [FromHeader] Guid AccessToken,
            [FromServices] ManipuladorUsuario manipulador
        )
        {
            comando.InformarOAccessToken(AccessToken);
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Este endpoint é responsável por criar um usuario com perfil administrativo ao qual lhe dara permissões de criar/editar uma empresa, usuario e obter relatorios administrativos
        /// </summary>
        /// <param name="comando"><b>Os campos no JSON deve ser preenchido das seguintes formas</b>
        /// <para> <b> *nome</b> => Este campo deve conter no minimo de 2 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *ultimoNome</b> => Este campo deve conter no minimo de 2 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *email</b> => Este campo deve conter no máximo de 150 caracteres </para>
        /// <para> <b> *senha</b> => Este campo deve conter no minimo de 4 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *celular</b> => Este campo deve ser informado (xx)xxxxx-xxxx</para>
        /// <para> <b> Observações</b> => Os Campos com * são obrigatórios para envio do comando para criar/gravar o usuario no repositorio</para>
        /// </param>
        /// <param name="manipulador"></param>
        /// <returns></returns>
        [Route("Super")]
        [HttpPost]
        public IActionResult Criar_usuario_super(
            [FromBody] ComandoCriarUsuarioSuper comando,
            [FromServices] ManipuladorUsuario manipulador
        )
        {
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Este endpont é responsável por editar dados do usuario como nome, ultimo nome, celular e status
        /// </summary>
        /// <param name="comando"><b>Os campos no JSON deve ser preenchido das seguintes formas</b>
        /// <para> <b> *nome</b> => Este campo deve conter no minimo de 2 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *ultimoNome</b> => Este campo deve conter no minimo de 2 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *email</b> => Este campo deve conter no máximo de 150 caracteres </para>
        /// <para> <b> *senha</b> => Este campo deve conter no minimo de 4 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *celular</b> => Este campo deve ser informado (xx)xxxxx-xxxx</para>
        /// <para> <b> Observações</b> => Os Campos com * são obrigatórios para envio do comando para atualizar/editar o usuario no repositorio</para>
        /// <para>Não é possivel alterar o email do usuario, pois atraves dele que é feito o login na aplcação, e a senha deve ser preenchida com a de login, para alterar a mesma usará outro endpoint</para>
        /// </param>
        /// <param name="AccessToken"></param>
        /// <param name="manipulador"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public IActionResult Editar_usuario(
           [FromBody] ComandoEditarUsuario comando,
           [FromHeader] Guid AccessToken,
           [FromServices] ManipuladorUsuario manipulador
        )
        {
            comando.InformarOAccessToken(AccessToken);
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Este endpont é responsável por alterar a senha do usuario
        /// </summary>
        /// <param name="comando"><b>Os campos no JSON deve ser preenchido das seguintes formas</b>
        /// <para> <b> *id</b> => identificador do usuario</para>
        /// <para> <b> *senhaAntiga</b> => Este campo deve conter no minimo de 4 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> *senhaNova</b> => Este campo deve conter no minimo de 4 caracteres e máximo de 60 caracteres</para>
        /// <para> <b> Observações</b> => Os Campos com * são obrigatórios para envio do comando para atualizar/editar o usuario no repositorio</para>
        /// </param>
        /// <param name="AccessToken"></param>
        /// <param name="manipulador"></param>
        /// <returns></returns>
        [Route("AlterarSenha")]
        [HttpPut]
        public IActionResult Editar_senha_usuario(
           [FromBody] ComandoEditarSenhaUsuario comando,
           [FromHeader] Guid AccessToken,
           [FromServices] ManipuladorUsuario manipulador
        )
        {
            comando.InformarOAccessToken(AccessToken);
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
