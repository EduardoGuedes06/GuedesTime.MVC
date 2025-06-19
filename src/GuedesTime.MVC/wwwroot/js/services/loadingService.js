// -----------------------------------------------------------------------------
// loadingService.js
// Módulo central para gerenciar o estado do modal de carregamento global.
// -----------------------------------------------------------------------------

const modalElement = document.getElementById('global-loading-modal');
const loadingTextElement = modalElement?.querySelector('.loading-text');

let requestCount = 0;
let timeoutId = null;
const DEFAULT_MESSAGE = 'Carregando...';

function show(message = DEFAULT_MESSAGE) {
    console.log(`loadingService.show() foi chamado com a mensagem: "${message}"`);
    console.trace("Rastreamento de quem chamou:");
    if (!modalElement) {
        console.error("Elemento do modal de carregamento global não encontrado. Verifique o ID 'global-loading-modal'.");
        return;
    }
    requestCount++;
    if (requestCount === 1) {

        if (loadingTextElement) {
            loadingTextElement.textContent = message;
        }
        timeoutId = setTimeout(() => {
            document.body.setAttribute('aria-busy', 'true');
            modalElement.hidden = false;
        }, 300);
    }
}

function hide() {
    if (!modalElement) return;
    requestCount = Math.max(0, requestCount - 1);
    if (requestCount === 0) {
        clearTimeout(timeoutId);
        modalElement.hidden = true;
        document.body.removeAttribute('aria-busy');
        if (loadingTextElement) {
            loadingTextElement.textContent = DEFAULT_MESSAGE;
        }
    }
}


export const loadingService = {
    show,
    hide
};