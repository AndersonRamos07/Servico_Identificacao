using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Comandos.Empresas;
using Identificacao.Dominio.Entidades;
using Identificacao.Dominio.Manipuladores;
using Identificacao.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Identificacao.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class EmpresaController : ControllerBase
    {
        /// <summary>
        /// Este endpoint é responsável por listar a(s) empresa(s) que o usuario administrador tem registrado no sistema
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="repositorio"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public Empresas ObterEmpresaPorId([FromHeader] Guid accessToken, [FromServices] IRepositorioEmpresa repositorio)
        {
            return repositorio.ObterEmpresaPorId(repositorio.ObterEmpresaPorToken(accessToken));
        }

        /// <summary>
        /// Este endpoint é responsável por criar uma empresa no sistema vinculado ao usuario administrador
        /// </summary>
        /// <param name="comando">
        /// <para> <b> *codigoRegimeTributavel</b> => Deve-se preencher os seguintes codigos: 1 - Simples Nacional; 2 - Simples Nacional - excesso de receita bruta; 3 - Regime Nacional; </para>
        /// <para> <b> *cnpj</b> => Este campo deve ser informado xx.xxx.xxx/xxxx-xx </para>
        /// <para> <b> *razaoSocial</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *telefone</b> => Este campo deve ser informado (xx)xxxxx-xxxx </para>
        /// <para> <b> *cep</b> => EDeve ser preencido da seguinte mascara xxxxx-xxx </para>
        /// <para> <b> *logradouro</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *numero</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> complemento</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *bairro</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *codCidade</b> => Este campo deve conter exatos de 07 caracteres, pode ser localizado este código através da api viacep </para>
        /// <para> <b> *cidade</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *uf</b> => Este campo deve conter no máximo de 02 caracteres </para>
        /// <para> <b> *inscricaoEstadual</b> => Este campo deve conter no máximo de 15 caracteres </para>
        /// <para> <b> *idUsuario</b> => Este campo é referente ao usuario que fez o cadastro</para>
        /// </param>
        /// <param name="manipulador"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult CriarEmpresa([FromBody] ComandoCriarEmpresa comando, [FromServices] ManipuladorEmpresa manipulador)
        {
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Este endpoint é responsável por editar uma empresa no sistema vinculado ao usuario administrador
        /// </summary>
        /// <param name="comando">
        /// <para> <b> *cnpj</b> => Este campo deve ser informado xx.xxx.xxx/xxxx-xx </para>
        /// <para> <b> *razaoSocial</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *telefone</b> => Este campo deve ser informado (xx)xxxxx-xxxx </para>
        /// <para> <b> *cep</b> => EDeve ser preencido da seguinte mascara xxxxx-xxx </para>
        /// <para> <b> *logradouro</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *numero</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> complemento</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *bairro</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *codCidade</b> => Este campo deve conter exatos de 07 caracteres, pode ser localizado este código através da api viacep </para>
        /// <para> <b> *cidade</b> => Este campo deve conter no máximo de 60 caracteres </para>
        /// <para> <b> *uf</b> => Este campo deve conter no máximo de 02 caracteres </para>
        /// <para> <b> *inscricaoEstadual</b> => Este campo deve conter no máximo de 15 caracteres </para>
        /// <para> <b> *idUsuario</b> => Este campo é referente ao usuario que fez o cadastro</para>
        /// </param>
        /// <param name="manipulador"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IActionResult EditarEmpresa([FromBody] ComandoEditarEmpresa comando, [FromServices] ManipuladorEmpresa manipulador)
        {
            var result = (CommandResultGeneric)manipulador.handle(comando);
            if (!result.Sucesso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
