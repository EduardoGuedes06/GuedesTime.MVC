
export function initBuscaCep() {
    const cepInput = document.getElementById('Endereco_Cep');
    if (!cepInput) return;

    cepInput.addEventListener('blur', (event) => {
        const cep = event.target.value.replace(/\D/g, '');

        if (cep.length !== 8) {
            return;
        }
        const logradouroInput = document.getElementById('Endereco_Logradouro');
        const bairroInput = document.getElementById('Endereco_Bairro');
        const cidadeInput = document.getElementById('Endereco_Cidade');
        const estadoInput = document.getElementById('Endereco_Estado');
        logradouroInput.value = "...";
        bairroInput.value = "...";
        cidadeInput.value = "...";
        estadoInput.value = "...";
        fetch(`https://viacep.com.br/ws/${cep}/json/`)
            .then(response => response.json())
            .then(data => {
                if (data.erro) {
                    alert('CEP não encontrado.');
                    logradouroInput.value = "";
                    bairroInput.value = "";
                    cidadeInput.value = "";
                    estadoInput.value = "";
                } else {
                    logradouroInput.value = data.logradouro;
                    bairroInput.value = data.bairro;
                    cidadeInput.value = data.localidade;
                    estadoInput.value = data.uf;
                }
            })
            .catch(error => {
                console.error('Erro ao buscar CEP:', error);
                alert('Não foi possível buscar o CEP.');
            });
    });
}

function validarCNPJ(cnpj) {
    if (cnpj.length !== 14 || /^(\d)\1{13}$/.test(cnpj)) return false;
    let tamanho = cnpj.length - 2, numeros = cnpj.substring(0, tamanho), digitos = cnpj.substring(tamanho), soma = 0, pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }
    let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0)) return false;
    tamanho++;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    return resultado == digitos.charAt(1);
}

export function initValidacaoCnpj() {
    const cnpjInput = document.getElementById('Cnpj');
    const cnpjError = document.getElementById('cnpj-error');

    if (!cnpjInput || !cnpjError) return;

    cnpjInput.addEventListener('input', () => {
        const cnpjValue = cnpjInput.value.replace(/\D/g, "");

        if (cnpjValue.length > 0 && cnpjValue.length < 14) {
            cnpjError.textContent = "CNPJ incompleto.";
        } else if (cnpjValue.length === 14 && !validarCNPJ(cnpjValue)) {
            cnpjError.textContent = "CNPJ inválido.";
        } else {
            cnpjError.textContent = "";
        }
    });
}