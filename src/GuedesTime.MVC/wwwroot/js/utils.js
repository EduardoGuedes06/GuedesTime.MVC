function handleCepInput(cepInput) {
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

function initializeToggleSwitch(visualCheckbox) {
    const form = visualCheckbox.closest('form');
    if (!form) {
        console.error('Toggle switch não está dentro de um formulário.', visualCheckbox);
        return;
    }

    const classNamePattern = /^campo-.*-toggle$/;
    const targetClassName = Array.from(visualCheckbox.classList).find(cls => classNamePattern.test(cls));

    if (!targetClassName) {
        console.error("Nenhuma classe correspondente ao padrão 'campo-X-toggle' foi encontrada.", visualCheckbox);
        return;
    }

    const propertyName = targetClassName.replace('campo-', '').replace('-toggle', '');
    const hiddenInputId = `hidden-${propertyName}-value`;
    const hiddenInput = form.querySelector(`#${hiddenInputId}`);

    if (!hiddenInput) {
        console.error(`Campo hidden com ID "${hiddenInputId}" não encontrado no formulário.`, form);
        return;
    }

    const initialValue = visualCheckbox.dataset.initialValue === 'true';
    visualCheckbox.checked = initialValue;

    const updateHiddenValue = () => {
        hiddenInput.value = visualCheckbox.checked;
    };

    updateHiddenValue();
    visualCheckbox.addEventListener('change', updateHiddenValue);
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

function handleFiltroInput(filtroInput) {

    debugger

    const filtroValue = filtroInput.value;

    if (filtroValue.length > 50) {
        filtroInput.value = filtroValue.slice(0, 50);
        const errorId = filtroInput.getAttribute('data-error-id');
        if (errorId) {
            const errorSpan = document.getElementById(errorId);
            if (errorSpan) errorSpan.textContent = "O filtro pode ter no máximo 50 caracteres.";
        }
        return;
    }

    const errorId = filtroInput.getAttribute('data-error-id');
    if (errorId) {
        const errorSpan = document.getElementById(errorId);
        if (errorSpan) errorSpan.textContent = "";
    }
}

function handleOrdinalUnicoInput(input) {
    let valor = input.value.replace(/\D/g, '');
    if (valor.length > 1) {
        valor = valor.charAt(0);
    }
    if (valor === '0') {
        valor = '';
    }
    input.value = valor ? valor + 'º' : '';
}

function handleOrdinalMultiploInput(input) {
    const errorSpan = document.getElementById(input.dataset.errorId);
    const numeros = input.value.match(/\d/g) || [];
    const numerosValidos = [...new Set(numeros)]
        .map(n => parseInt(n, 10))
        .filter(n => n >= 1 && n <= 9);

    let numerosLimitados = numerosValidos;
    if (numerosValidos.length > 10) {
        numerosLimitados = numerosValidos.slice(0, 10);
        if (errorSpan) errorSpan.textContent = "Limite de 10 itens atingido.";
    } else {
        if (errorSpan) errorSpan.textContent = "";
    }

    const valorFinal = numerosLimitados
        .sort((a, b) => a - b)
        .map(n => n + 'º')
        .join(', ');

    input.value = valorFinal;
}

function initializeClearInputButtons() {
    document.addEventListener('click', function (event) {
        const clearButton = event.target.closest('.js-clear-input-btn');
        if (!clearButton) return;
        event.preventDefault();

        const wrapper = clearButton.closest('.input-with-button');
        if (!wrapper) return;

        const inputToClear = wrapper.querySelector('input');
        if (inputToClear) {
            inputToClear.value = '';
            inputToClear.focus();
            inputToClear.dispatchEvent(new Event('input', { bubbles: true }));
        }
    });
}


function handleNumeroInput(numeroInput) {
    let valor = numeroInput.value.replace(/\D/g, '');
    if (valor.length > 10) {
        valor = valor.substring(0, 10);
    }
    numeroInput.value = valor;
}

export {
    handleCepInput,
    initializeToggleSwitch,
    handleCnpjInput,
    handleNomeInput,
    handleNumeroInput,
    handleFiltroInput,
    handleOrdinalUnicoInput,
    handleOrdinalMultiploInput,
    initializeClearInputButtons
};