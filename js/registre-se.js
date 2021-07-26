const baseUrl = 'http://192.168.0.117:3000/v1/Usuarios/Super';

const cache = sessionStorage;

const cabecalho =
{
  headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
  method: 'POST',
};

function start(){
    document.querySelector('.email').focus();
};

function inserirDados() {
  var novousuario = new Object();
    novousuario.nome = document.querySelector('.nome').value;
    novousuario.ultimoNome = document.querySelector('.ultimoNome').value;
    novousuario.email = document.querySelector('.email').value;
    novousuario.senha = document.querySelector('.senha').value;
    novousuario.celular = document.querySelector('.celular').value;      
  return enviarDados(novousuario);
};
  
function enviarDados(novoUsuario) {
  cabecalho.body = JSON.stringify(novoUsuario);
  fetch(baseUrl, cabecalho)
  .then(response => response.json())
  .then(data => tratamentos(data))
  .catch(err => console.log(err.message));
};

function tratamentos(json){
  if(json.sucesso != false){
    cache.setItem('sirius-nome', json.dados.nome);
    cache.setItem('sirius-email', json.dados.email);
    cache.setItem('sirius-id', json.dados.id);    
    cache.setItem('sirius-login', true);
    console.warn("RESPOSTA: ", json);
    let par = confirm("PARABÉNS " + json.dados.nome + ", seu cadastro foi feito com sucesso! \n\nRealize seu primeiro acesso ao nosso sistema!");
    if(par){
      window.open('./login.html');
    }
    else{             //UM ALERT CONTENDO UMA PRÉ-BOAS-VINDAS
      console.warn("Foi com o ID: ", json.dados.id);
      console.warn("E de Nome: ", json.dados.nome);
    }
  }
  else{             //AQUI FICARÁ OS ERROS QUE APARECERÃO NO MODAL-DE-ERROS
    for(var i = 0; i < json.dados.length; i++){
      let mensagem = json.dados[i];
      console.info("ERRO(S)", mensagem.message)
    }
  }
};