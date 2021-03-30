using Identificacao.Dominio.Entidades;
using System;
using System.Linq.Expressions;

namespace Identificacao.Dominio.Consultas
{
    public static class Consultasusuarios
    {
        public static Expression<Func<Usuarios, bool>> Obter_usuario_email(string email)
        {
            return x => x.Email == email;
        }

        public static Expression<Func<Usuarios, bool>> Obter_usuario_id(int id)
        {
            return x => x.Id == id;
        }

        public static Expression<Func<Usuarios, bool>> Obter_usuario_id_empresa(int idEmpresa)
        {
            return x => x.IdEmpresa == idEmpresa;
        }


    }
}
