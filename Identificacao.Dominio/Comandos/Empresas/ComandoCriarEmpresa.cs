using Flunt.Notifications;
using Flunt.Validations;
using Identificacao.Dominio.Comandos.Contratos;

namespace Identificacao.Dominio.Comandos.Empresas
{
    public class ComandoCriarEmpresa : Notifiable, ICommand
    {
        public ComandoCriarEmpresa() { }

        public ComandoCriarEmpresa(int codigoRegimeTributavel, string cnpj, string razaoSocial, string email, string telefone, string cep, string logradouro, string numero, string complemento, string bairro, string codCidade, string cidade, string uf, string inscricaoEstadual, int idUsuario)
        {
            CodigoRegimeTributavel = codigoRegimeTributavel;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            Telefone = telefone;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CodCidade = codCidade;
            Cidade = cidade;
            Uf = uf;
            InscricaoEstadual = inscricaoEstadual;
            IdUsuario = idUsuario;
        }

        public int CodigoRegimeTributavel { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CodCidade { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string InscricaoEstadual { get; set; }
        public int IdUsuario { get; set; }

        private void CamposObrigatorios()
        {
            AddNotifications(new Contract()
                            .Requires()
                            .IsGreaterOrEqualsThan(CodigoRegimeTributavel,1, "CodigoRegimeTributavel", "Deve-se preencher os seguintes codigos: 1 - Simples Nacional; 2 - Simples Nacional - excesso de receita bruta; 3 - Regime Nacional;")
                            .IsNotNullOrEmpty(Cnpj, "Cnpj", "É obrigatório CNPJ")
                            .IsNotNullOrEmpty(RazaoSocial, "RazaoSocial", "É obrigatório Razão Social")
                            .IsNotNullOrEmpty(Telefone, "Telefone", "É obrigatório Telefone")
                            .IsNotNullOrEmpty(Cep, "Cep", "É obrigatório Cep")
                            .IsNotNullOrEmpty(Logradouro, "Logradouro", "É obrigatório Logradouro")
                            .IsNotNullOrEmpty(Numero, "Numero", "É obrigatório Numero")
                            .IsNotNullOrEmpty(Bairro, "Bairro", "É obrigatório Bairro")
                            .IsNotNullOrEmpty(CodCidade, "CodCidade", "É obrigatório CodCidade")
                            .IsNotNullOrEmpty(Cidade, "Cidade", "É obrigatório Cidade")
                            .IsNotNullOrEmpty(Uf, "Uf", "É obrigatório Uf"));
        }

        private void ConfiguracaoDosTamanhosDosCamposRecebidos()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(Cnpj, 18, "CNPJ", "O CNPJ informado deve conter 18 caracteres")
                .HasMaxLen(RazaoSocial, 60, "Razão Social", "A razão social informada deve conter no maximo de 60 caracteres")
                .HasMinLen(Logradouro, 2, "Logradouro", "O logradouro informado deve conter no minimo de 2 caracteres")
                .HasMaxLen(Logradouro, 60, "Logradouro", "O logradouro informado deve conter no máximo de 60 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(Complemento, 60, "Complemento", "O complemento informado deve conter no maximo de 60 caracteres")
                .HasExactLengthIfNotNullOrEmpty(Cep, 9, "Cep", "O cep deve conter 9 carecteres")
                .HasMaxLen(Numero, 60, "Numero", "O numero imformado deve conter no maximo de caracteres")
                .HasMinLen(Bairro, 2, "Bairro", "O bairro informado deve conter no minimo de 2 caracteres")
                .HasMaxLen(Bairro, 60, "Bairro", "O bairro informado deve conter no maximo de 60 caracteres")
                .HasLen(CodCidade, 7, "Codigo Cidade", "O codigo da cidade informado deve conter somente 7 caraxteres")
                .HasMinLen(Cidade, 2, "Cidade", "A cidade informada deve conter no minimo de 2 caracteres")
                .HasMaxLen(Cidade, 60, "Cidade", "A cidade informada deve conter no maximo de 60 caracteres")
                .HasLen(Uf, 2, "Uf", "O estado informado deve conter somente 2 caracteres")
                .HasMinLen(InscricaoEstadual, 6, "Inscrição Estadual", "A inscrição estadual informada deve conter no minimo de 6 caracteres")
                .HasMaxLen(InscricaoEstadual, 15, "Inscrição Estadual", "A inscrição estadual informada deve conter no maximo de 14 caracteres"));
        }

        public void Validate()
        {
            CamposObrigatorios();
            ConfiguracaoDosTamanhosDosCamposRecebidos();
        }
    }
}
