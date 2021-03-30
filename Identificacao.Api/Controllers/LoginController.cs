using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Comandos.Usuarios;
using Identificacao.Dominio.Manipuladores;
using Microsoft.AspNetCore.Mvc;

namespace Identificacao.Api.Controllers
{
    [ApiController]
    [Route("v1/Login")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Este endpoint é responsável pelo acesso ao sistema
        /// </summary>
        /// <param name="comando"><b>Os campos no JSON deve ser preenchido das seguintes formas</b>
        /// <para> <b> *email</b> => Este campo deve conter no máximo de 150 caracteres </para>
        /// <para> <b> *senha</b> => Este campo deve conter no minimo de 4 caracteres e máximo de 60 caracteres</para>
        /// </param>
        /// <param name="manipulador"></param>
        /// <returns>aaa</returns>
        [Route("")]
        [HttpPost]
        public IActionResult Logar_sistema(
            [FromBody] ComandoLogarNoSistema comando,
            [FromServices] ManipuladorUsuario manipulador
        )
        {
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
