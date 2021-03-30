using Identificacao.Dominio.Entidades;
using System;
using System.Linq.Expressions;

namespace Identificacao.Dominio.Consultas
{
    public static class ConsultasEmpresas
    {
        public static Expression<Func<Empresas, bool>> ObterEmpresaPorId(int id)
        {
            return x => x.Id == id;
        }

        public static Expression<Func<Empresas, bool>> ObterEmpresaPorToken(Guid Token)
        {
            return x => x.AccessToken == Token;
        }

        public static Expression<Func<Empresas, bool>> ObterEmpresaPorCnpj(string cnpj)
        {
            return x => x.Cnpj == cnpj;
        }
    }
}
