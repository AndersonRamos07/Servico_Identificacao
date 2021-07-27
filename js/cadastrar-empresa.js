const baseUrl = 'http://192.168.0.117:3000/v1/Empresa';

const cache = sessionStorage;
var idDoCliente = cache.getItem('sirius-id');
var idUser = parseInt(idDoCliente);

const cabecalho =
{
  headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
  method: 'POST',
};

//$(".codigoRegimeTributavel").mask("0");
$(".cnpj").mask("00.000.000/0000-00");
$(".telefone").mask("(00)00000-0000");
$(".cep").mask("00000-000");

function start()
{
  if(idDoCliente != null && idDoCliente != undefined)
  {
    document.querySelector('.idUsuario').value = idDoCliente;
  }
  document.querySelector('.codigoRegimeTributavel').focus();
};

function inserirDados()
{
  var novaEmpresa = new Object();
    novaEmpresa.codigoRegimeTributavel = parseInt(document.querySelector('.codigoRegimeTributavel').value);
    novaEmpresa.cnpj = document.querySelector('.cnpj').value;
    novaEmpresa.razaoSocial = document.querySelector('.razaoSocial').value;
    novaEmpresa.telefone = document.querySelector('.telefone').value;
    novaEmpresa.cep = document.querySelector('.cep').value;
    novaEmpresa.logradouro = document.querySelector('.logradouro').value;
    novaEmpresa.numero = document.querySelector('.numero').value;
    novaEmpresa.complemento = document.querySelector('.complemento').value;
    novaEmpresa.bairro = document.querySelector('.bairro').value;
    novaEmpresa.codCidade = document.querySelector('.codCidade').value;
    novaEmpresa.cidade = document.querySelector('.cidade').value;
    novaEmpresa.uf = document.querySelector('.uf').value;
    novaEmpresa.inscricaoEstadual = document.querySelector('.inscricaoEstadual').value;
    novaEmpresa.idUsuario =parseInt(document.querySelector('.idUsuario').value);     
  return enviarDados(novaEmpresa);
};

function enviarDados(novaempresa)
{
  cabecalho.body = JSON.stringify(novaempresa);
  fetch(baseUrl, cabecalho)
  .then(response => response.json())
  .then(data => tratamentos(data))
  .catch(err => alert(err.message));
};

function tratamentos(json)
{
  if(json.sucesso)
  {
    Swal.fire({
      icon: 'success',
      title: json.mensagem,
      text: 'Gostaria de cadastrar mais uma empresa?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: `Sim!`,
      denyButtonText: `Agora não!`,
    }).then((result) =>
    {
      if(result.isConfirmed)
      {
        Swal.fire('OK!', 'Retornando para a área de cadastro de empresa...', 'info');
        location.href='./cadastrar-empresa.html';
      }
      else if(result.isDenied)
      {
        Swal.fire('Certo!', 'Indo para a área de início...', 'info');
        location.href='../dashboard/index.html';
      }
    })
  }
  else
  {
    var erro_s = '';
    for(var i = 0; i < json.dados.length; i++)
    {
      erro_s += "<li>" + json.dados[i].property + ' - ' + json.dados[i].message + "</li>";
    }
    Swal.fire({
      width: 900,
      icon: 'warning',
      title: json.mensagem,
      html: '<h5>Alguns campos precisam ser preenchidos:</h5> <ul style="list-style-type:none">' + erro_s + '</ul>'
    })
  }            //AQUI FICARÁ OS ERROS QUE APARECERÃO NO MODAL-DE-ERROS
};