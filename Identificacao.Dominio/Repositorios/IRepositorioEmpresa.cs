using Identificacao.Dominio.Entidades;
using System;

namespace Identificacao.Dominio.Repositorios
{
    public interface IRepositorioEmpresa
    {
        void CriarEmpresa(Empresas empresa);
        void EditarEmpresa(Empresas empresa);
        Empresas ObterEmpresaPorId(int id);
        Empresas ObterEmpresaPorCnpj(string cnpj);
        int ObterEmpresaPorToken(Guid token);
        bool VerificarSeUsuarioTemPermissaoSuper(int idUsuario);
        void AtualizaUsuarioComEmpresa(int idUsuario, int idEmpresa);
    }
}
