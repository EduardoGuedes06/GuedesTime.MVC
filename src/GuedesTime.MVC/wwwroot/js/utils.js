function handleCepInput(cepInput) {
    debugger
    const form = cepInput.closest('form');
    if (!form) return;
    let cep = cepInput.value.replace(/\D/g, '');
    if (cep.length > 5) {
        cep = cep.substring(0, 5) + '-' + cep.substring(5, 8);
    }
    cepInput.value = cep;
    if (cep.length !== 9) return;
    const logradouroInput = form.querySelector('.campo-logradouro');
    const bairroInput = form.querySelector('.campo-bairro');
    const cidadeInput = form.querySelector('.campo-cidade');
    const estadoInput = form.querySelector('.campo-estado');
    if (!logradouroInput || !bairroInput || !cidadeInput || !estadoInput) return;
    logradouroInput.value = "...";
    bairroInput.value = "...";
    cidadeInput.value = "...";
    estadoInput.value = "...";
    fetch(`https://viacep.com.br/ws/${cep.replace('-', '')}/json/`)
        .then(response => response.json())
        .then(data => {
            logradouroInput.value = data.erro ? "" : data.logradouro;
            bairroInput.value = data.erro ? "" : data.bairro;
            cidadeInput.value = data.erro ? "" : data.localidade;
            estadoInput.value = data.erro ? "" : data.uf;
        });
}

function validarCNPJ(cnpj) {
    if (!cnpj || cnpj.length !== 14 || /^(\d)\1{13}$/.test(cnpj)) return false;

    let tamanho = cnpj.length - 2;
    let numeros = cnpj.substring(0, tamanho);
    let digitos = cnpj.substring(tamanho);
    let soma = 0;
    let pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    let resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado != digitos.charAt(0)) return false;

    tamanho++;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    return resultado == digitos.charAt(1);
}

function handleCnpjInput(cnpjInput) {
    const cnpjError = document.getElementById('cnpj-error');
    if (!cnpjError) return true;
    let cnpj = cnpjInput.value.replace(/\D/g, '');
    if (cnpj.length > 14) cnpj = cnpj.substring(0, 14);
    if (cnpj.length <= 14) {
        if (cnpj.length > 2) cnpj = cnpj.replace(/^(\d{2})(\d)/, '$1.$2');
        if (cnpj.length > 5) cnpj = cnpj.replace(/^(\d{2})\.(\d{3})(\d)/, '$1.$2.$3');
        if (cnpj.length > 8) cnpj = cnpj.replace(/\.(\d{3})(\d)/, '.$1/$2');
        if (cnpj.length > 12) cnpj = cnpj.replace(/(\d{4})(\d)/, '$1-$2');
    }
    cnpjInput.value = cnpj;

    const cnpjValue = cnpj.replace(/\D/g, '');

    if (cnpjValue.length > 0 && cnpjValue.length < 14) {
        cnpjError.textContent = "CNPJ incompleto.";
        return false;
    } else if (cnpjValue.length === 14 && !validarCNPJ(cnpjValue)) {
        cnpjError.textContent = "CNPJ inválido.";
        return false;
    } else {
        cnpjError.textContent = "";
        return true;
    }
}
function handleNomeInput(nomeInput) {
    const nameError = document.getElementById('name-error');
    const nameValue = nomeInput.value;
    const regex = /^[A-Za-zÀ-ÖØ-öø-ÿ. ]*$/;
    if (!regex.test(nameValue)) {
        if (nameError) nameError.textContent = "O nome só pode conter letras e ponto.";
        nomeInput.value = nameValue.replace(/[^A-Za-zÀ-ÖØ-öø-ÿ. ]/g, "");
        return;
    }
    const correctedName = nameValue
        .toLowerCase()
        .replace(/(^|[\s.])([a-zà-öø-ÿ])/g, (match, sep, char) => sep + char.toUpperCase());
    nomeInput.value = correctedName;
    if (nameError) nameError.textContent = "";
}

function handleNumeroInput(numeroInput) {
    let valor = numeroInput.value.replace(/\D/g, '');
    if (valor.length > 10) {
        valor = valor.substring(0, 10);
    }

    numeroInput.value = valor;
}
export { handleCepInput, handleCnpjInput, handleNomeInput, handleNumeroInput };