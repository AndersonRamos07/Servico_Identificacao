const cache = sessionStorage;
const siriusEmail = cache.getItem('sirius-email');

function obterEmail()
{
  if(siriusEmail != null)
  {
    document.querySelector('.email').value = siriusEmail;
    document.querySelector('.senha').focus();
  }
  else
  {
    document.querySelector('.email').focus();
  }
};

function validarLogin(dados)
{
  const URL = 'http://192.168.0.117:3000/v1/Login';
  const CABECALHO =
  {
    method:'POST',
    headers:
    {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body : dados
  };
  return fetch( URL , CABECALHO );
};

async function logar()
{
  const container = document.querySelector('.container');
  const dadosLogin = new Object();
  dadosLogin.email = document.querySelector('.email').value;
  dadosLogin.senha = document.querySelector('.senha').value;
  const logOn = JSON.stringify(dadosLogin);
  container.innerHTML = '<div class="spinner-border" style="width: 3rem; height: 3rem;" role="status"><span class="sr-only">Loading...</span></div>';
  try
  {
    const loginResponse = await validarLogin(logOn);
    const data = await loginResponse.json();
    if(data.sucesso)
    {
      const direcEmpresa = confirm('Olá ' + data.dados.nome + ' gostaria de cadastrar uma empresa?');
      if(direcEmpresa)
      {
         window.location = './cadastrar-empresa.html';
      }
      else
      {
        window.location = '../dashboard/index.html';
      }
    }
    else
    {
      console.log("ELSE DATA", data)
    }
  }
  catch (err)
  {
    console.log("CATCH ERRO", err);
  }
};
    // Swal.fire({
    //   icon: 'success',
    //   title: json.mensagem,
    //   text: 'Olá ' + json.dados.nome + ' gostaria de cadastrar uma empresa?',
    //   showDenyButton: true,
    //   confirmButtonText: `Certo, vamos lá!`,
    //   denyButtonText: `Agora não, obrigado!`
    // }).then((result) =>
    // {
    //   if(result.isConfirmed)
    //   {
    //     Swal.fire('Muito bem!', 'vamos cadastrar uma empresa...', 'info');
    //     location.href='./cadastrar-empresa.html';
    //   }
    //   else if(result.isDenied)
    //   {
    //     Swal.fire('Certo!', 'Indo para a área de início...', 'info');
    //     location.href='../dashboard/index.html';
    //   }
    // })