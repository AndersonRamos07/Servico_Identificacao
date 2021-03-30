using Identificacao.Dominio.Comandos;
using Identificacao.Dominio.Consultas;
using Identificacao.Dominio.Entidades;
using Identificacao.Dominio.Repositorios;
using Identificacao.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Identificacao.Infra.Repositorios
{
    public class RepositorioEmpresa : IRepositorioEmpresa
    {
        private readonly DadosContexto _dados;

        public RepositorioEmpresa(DadosContexto dados)
        {
            _dados = dados;
        }

        public void AtualizaUsuarioComEmpresa(int idUsuario, int idEmpresa)
        {
            // Recupera o usuario através do identificador
            var usuario = _dados.usuarios.FirstOrDefault(x => x.Id == idUsuario);

            // Atribui id da empresa
            usuario.AtribuiIdEmpresa(idEmpresa);

            // Realiza o processo de atualização no banco de dados 
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

        public void CriarEmpresa(Empresas empresa)
        {
            _dados.Empresas.Add(empresa);
            try
            {
                _dados.SaveChanges();
            }
            catch (Exception e)
            {
                new CommandResultGeneric(false, "Erro ao cadastrar dados no nosso banco de dados contate a SoftClver", e);
            }
        }

        public void EditarEmpresa(Empresas empresa)
        {
            _dados.Entry<Empresas>(empresa).State = EntityState.Modified;
            try
            {
                _dados.SaveChanges();
            }
            catch (Exception e)
            {
                new CommandResultGeneric(false, "Erro ao cadastrar dados no nosso banco de dados contate a SoftClver", e);
            }
        }


        public Empresas ObterEmpresaPorCnpj(string cnpj)
        {
            return _dados.Empresas.AsNoTracking().FirstOrDefault(ConsultasEmpresas.ObterEmpresaPorCnpj(cnpj));
        }

        public Empresas ObterEmpresaPorId(int id)
        {
            return _dados.Empresas.AsNoTracking().FirstOrDefault(ConsultasEmpresas.ObterEmpresaPorId(id));
        }

        public int ObterEmpresaPorToken(Guid token)
        {
            // Recupera a empresa através do token
            var empresa = _dados.Empresas.FirstOrDefault(ConsultasEmpresas.ObterEmpresaPorToken(token));

            // Verifica se foi retornado algo, caso negativo retorna 0
            if (empresa == null)
                return 0;

            // Caso for localizado retorne o identificador da empresa
            return empresa.Id;
        }

        public bool VerificarSeUsuarioTemPermissaoSuper(int idUsuario)
        {
            // Recupera o usuario através do identificador
            var usuario = _dados.usuarios.FirstOrDefault(x => x.Id == idUsuario);

            // Verifica se foi localizado algum usuario
            if (usuario == null)
                return false;

            return usuario.VerificaPermissaoUsuario();
        }

    }
}
