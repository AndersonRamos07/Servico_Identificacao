using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Consultas;
using Identificacao.Dominio.Entidades;
using Identificacao.Dominio.Repositorios;
using Identificacao.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public bool Criar_usuario(Usuarios usuario)
        {
            //Cria novos dados no repositorio com EF
            _dados.Entry<Usuarios>(usuario).State = EntityState.Added;
            // Executa o metodo Salvar no repositorio ao qual retorna um valor booleano informando se foi ou não possivel realizar a operação
            return SalvarDadosRepositorio().Result;
        }

        public bool Editar_usuario(Usuarios usuario)
        {
            // Atualiza os dados no repositorio com EF
            _dados.Entry<Usuarios>(usuario).State = EntityState.Modified;
            // Executa o metodo Salvar no repositorio ao qual retorna um valor booleano informando se foi ou não possivel realizar a operação
            return SalvarDadosRepositorio().Result;
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
            var empresa = _dados.Empresas.AsNoTracking().FirstOrDefault(x => x.AccessToken == accessToken);

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
            // Localiza um usuario pelo email
            return _dados.usuarios.AsNoTracking().FirstOrDefault(Consultasusuarios.Obter_usuario_email(email));
        }

        public Usuarios Obter_usuario_id(int id)
        {
            // Localiza o usuario pelo id
            return _dados.usuarios.AsNoTracking().FirstOrDefault(Consultasusuarios.Obter_usuario_id(id));
        }

        public Dictionary<string, bool> Obter_usuario_pot_id(Guid accessToken, int id)
        {
            // Localiza o usuario
            var user = Obter_usuario_id(id);
            // Localiza o id da empresa se é valido
            var token = ObterEmpresaPorAccessToken(accessToken);
            // Cria um novo diciionario para validação
            var validacao = new Dictionary<string, bool>();
            // Se o usuario for nulo ou id da empresa for 0 retorna a validação falsa, caso contrario retorna verdadeiro
            if (user == null || token == 0)
                validacao.Add("validacaoUsuario", false);
            else
                validacao.Add("validacaoUsuario", true);
            // Retorna a validação realizada
            return validacao;
        }

        public async Task<bool> SalvarDadosRepositorio()
        {
            try
            {
                //Salva os dados no repositorio de maneira assicrona e retorna true
                await _dados.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Retorna false caso apresente erro ao salvar algum dado no repositorio
                return false;
            }
        }
    }
}
