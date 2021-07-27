const baseUrl = 'http://192.168.0.117:3000/v1/Usuarios/Super';

const cache = sessionStorage;

const cabecalho =
{
  headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
  method: 'POST',
};

$(".celular").mask("(00)00000-0000");

function start()
{
  document.querySelector('.email').focus();
};

function inserirDados()
{
  var novousuario = new Object();
    novousuario.nome = document.querySelector('.nome').value;
    novousuario.ultimoNome = document.querySelector('.ultimoNome').value;
    novousuario.email = document.querySelector('.email').value;
    novousuario.senha = document.querySelector('.senha').value;
    novousuario.celular = document.querySelector('.celular').value;      
  return enviarDados(novousuario);
};
  
function enviarDados(novoUsuario)
{
  cabecalho.body = JSON.stringify(novoUsuario);
  fetch(baseUrl, cabecalho)
  .then(response => response.json())
  .then(data => tratamentos(data))
  .catch(err => alert(err.message));
};

function tratamentos(json)
{
  if(json.sucesso != false)
  {
    cache.setItem('sirius-nome', json.dados.nome);
    cache.setItem('sirius-email', json.dados.email);
    cache.setItem('sirius-id', json.dados.id);    
    cache.setItem('sirius-login', true);
    Swal.fire({
      icon: 'success',
      title: json.mensagem,
      text: 'Parabéns ' + json.dados.nome + ' seu cadastro foi realizado com sucesso!',
      showDenyButton: false,
      confirmButtonText: `Certo, vamos realizar o acesso!`,
    }).then((result) =>
    {
      if(result.isConfirmed)
      {
        Swal.fire('Muito bem!', 'vamos agora realizar o primeiro login ...', 'info');
        location.href='./login.html';
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
  }
};