using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Consultas;
using Identificacao.Dominio.Entidades;
using Identificacao.Dominio.Repositorios;
using Identificacao.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identificacao.Infra.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuario
    {
        private readonly DadosContexto _dados;
        private readonly IRepositorioEmpresa _repositorio;

        public RepositorioUsuarios(DadosContexto dados, IRepositorioEmpresa repositorio)
        {
            _dados = dados;
            _repositorio = repositorio;
        }

        public void Criar_usuario(Usuarios usuario)
        {
            _dados.usuarios.Add(usuario);
            try
            {
                _dados.SaveChanges();
            }
            catch (Exception e)
            {
                new CommandResultGeneric(false, "Erro ao cadastrar dados no nosso banco de dados contate a SoftClver", e);
            }
        }

        public void Editar_usuario(Usuarios usuario)
        {
            _dados.Entry<Usuarios>(usuario).State = EntityState.Modified;
            try
            {
                _dados.SaveChanges();
            }
            catch (Exception e)
            {
                new CommandResultGeneric(false, "Erro ao cadastrar dados no nosso banco de dados contate a SoftClver", e);
            }
        }

        public Guid ObterAccessToken(int idEmpresa)
        {
            // Busca empresa por acessToken
            var empresa = _repositorio.ObterEmpresaPorId(idEmpresa);

            //Se não localizar nenhuma empresa retorna Guid.Empty
            if (empresa == null)
                return Guid.Empty;

            // Se localizar retorna o AccessToken da empresa
            return empresa.AccessToken;
        }

        public int ObterEmpresaPorAccessToken(Guid accessToken)
        {
            // Busca empresa por acessToken
            var empresa = _dados.Empresas.FirstOrDefault(x => x.AccessToken == accessToken);

            //Se não localizar nenhuma empresa retorna 0
            if (empresa == null)
                return 0;

            // Se localizar retorna o id da empresa
            return empresa.Id;
        }

        public IEnumerable<Dictionary<string, string>> Obter_usuario(Guid accessToken)
        {
            //Cria uma nova lista
            var listaUsuarios = new List<Dictionary<string, string>>();

            // Busca o id da empresa atraves do metodo ObterEmpresaPorAccessToken()
            var idEmpresa = ObterEmpresaPorAccessToken(accessToken);

            //Se o id da empresa for igual a 0 ele retorna um dicionario com erro
            if (idEmpresa == 0)
            {
                //Cria o dicionario para envio
                var visualizacaoUsuarios = new Dictionary<string, string>();

                visualizacaoUsuarios.Add("erro", "O accessToken está incorreto");
                listaUsuarios.Add(visualizacaoUsuarios);
                return listaUsuarios;
            }

            // Se o id da empresa for maior que 0 ele ira realizar a busca dos usuarios cadastrado com o accessToken
            var usuarios = _dados.usuarios.AsNoTracking().Where(Consultasusuarios.Obter_usuario_id_empresa(idEmpresa)).ToList();

            // Adiciona as informações da lista listaUsuarios as informações de visualizacaoUsuarios
            foreach (var usuario in usuarios)
            {
                //Cria o dicionario para envio
                var visualizacaoUsuarios = new Dictionary<string, string>();

                // Adiciona os valores ao dicionario
                visualizacaoUsuarios.Add("id", usuario.Id.ToString());
                visualizacaoUsuarios.Add("nome", usuario.Nome);
                visualizacaoUsuarios.Add("ultimoNome", usuario.UltimoNome);
                visualizacaoUsuarios.Add("email", usuario.Email);
                visualizacaoUsuarios.Add("celular", usuario.Celular);
                visualizacaoUsuarios.Add("status", usuario.Status);
                listaUsuarios.Add(visualizacaoUsuarios);
            }

            // Retorna a lista com as informações do usuario
            return listaUsuarios;
        }

        public Usuarios Obter_usuario_email(string email)
        {
            return _dados.usuarios.AsNoTracking().FirstOrDefault(Consultasusuarios.Obter_usuario_email(email));
        }

        public Usuarios Obter_usuario_id(int id)
        {
            return _dados.usuarios.AsNoTracking().FirstOrDefault(Consultasusuarios.Obter_usuario_id(id));
        }

    }
}
