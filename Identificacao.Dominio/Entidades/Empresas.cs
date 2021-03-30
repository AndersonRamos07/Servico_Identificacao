using System;

namespace Identificacao.Dominio.Entidades
{
    public class Empresas : Entidade
    {
        public Empresas(int codigoRegimeTributavel, string cnpj, string razaoSocial, string cep, string logradouro, string numero, string complemento, string bairro, string codCidade, string cidade, string uf, string inscricaoEstadual)
        {
            CodigoRegimeTributavel = codigoRegimeTributavel;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CodCidade = codCidade;
            Cidade = cidade;
            Uf = uf;
            InscricaoEstadual = inscricaoEstadual;
            AccessToken = Guid.NewGuid();
        }

        public int CodigoRegimeTributavel { get; private set; }
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CodCidade { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public Guid AccessToken { get; private set; }

        public void EditarEmpresa(int crt, string cnpj, string razaoSocial, string cep, string logradouro, string numero, string complemento, string bairro, string codCidade, string cidade, string uf, string inscricaoEstadual)
        {
            CodigoRegimeTributavel = crt;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CodCidade = codCidade;
            Cidade = cidade;
            Uf = uf;
            InscricaoEstadual = inscricaoEstadual;
        }
    }
}
