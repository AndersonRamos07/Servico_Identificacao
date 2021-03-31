using Identificacao.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Identificacao.Dominio.Repositorios
{
    public interface IRepositorioUsuario
    {
        bool Criar_usuario(Usuarios usuario);
        bool Editar_usuario(Usuarios usuario);
        Usuarios Obter_usuario_email(string email);
        Usuarios Obter_usuario_id(int id);
        IEnumerable<Dictionary<string, string>> Obter_usuario(Guid accessToken);
        Guid ObterAccessToken(int idEmpresa);
        int ObterEmpresaPorAccessToken(Guid accessToken);
        Dictionary<string, bool> Obter_usuario_pot_id(Guid accessToken, int id);
    }
}
