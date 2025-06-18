// Espera o DOM carregar para mostrar notificações iniciais (toast)
document.addEventListener('DOMContentLoaded', () => {
    import('./ui.js').then(ui => {
        const successMessage = document.getElementById('tempdata-success-message')?.value;
        if (successMessage) ui.showToast(successMessage, 'success');

        const errorMessage = document.getElementById('tempdata-error-message')?.value;
        if (errorMessage) ui.showToast(errorMessage, 'error');
    });
});

// Listener global para cliques, lidando com abertura e fechamento de modais
document.addEventListener('click', function (event) {
    // Lógica para abrir o modal
    const modalTrigger = event.target.closest('[data-modal-url]');
    if (modalTrigger) {
        event.preventDefault();
        import('./ui.js').then(ui => {
            const url = modalTrigger.getAttribute('data-modal-url');
            const modal = document.getElementById('modal-global');
            const modalContent = document.getElementById('modal-global-content');
            if (!modal || !modalContent) return;

            modalContent.innerHTML = '<p style="text-align:center; padding: 20px;">Carregando...</p>';
            ui.openModal(modal);

            fetch(url)
                .then(response => response.text())
                .then(html => {
                    // 1. O HTML do modal é injetado na página.
                    modalContent.innerHTML = html;

                    // --- INÍCIO DA LÓGICA ADICIONADA ---
                    // 2. Imediatamente depois, procuramos e inicializamos o nosso toggle switch.
                    const visualCheckbox = modalContent.querySelector('.campo-ativo-toggle');
                    if (visualCheckbox) {
                        const initialValue = visualCheckbox.dataset.initialValue;
                        visualCheckbox.checked = (initialValue === 'true');

                        // Chama a função de manipulação uma vez para garantir que o valor
                        // inicial do campo oculto seja definido corretamente.
                        import('./utils.js').then(utils => {
                            if (utils.handleAtivoToggle) {
                                utils.handleAtivoToggle(visualCheckbox);
                            }
                        });
                    }
                    // --- FIM DA LÓGICA ADICIONADA ---
                })
                .catch(err => {
                    console.error('Erro ao carregar conteúdo do modal:', err);
                    modalContent.innerHTML = '<p style="text-align:center; padding: 20px; color: red;">Erro ao carregar conteúdo.</p>';
                });
        });
    }

    // Lógica para fechar o modal
    const closeTrigger = event.target.closest('[data-modal-close]');
    if (closeTrigger) {
        import('./ui.js').then(ui => {
            const modalToClose = closeTrigger.closest('.modal-overlay');
            if (modalToClose) ui.closeModal(modalToClose);
        });
    }
});

// Listener global para o evento 'input', para mascarar e validar campos em tempo real
document.addEventListener('input', function (event) {
    if (event.target.classList.contains('campo-cep')) {
        import('./utils.js').then(utils => utils.handleCepInput(event.target));
    }
    if (event.target.classList.contains('campo-cnpj')) {
        import('./utils.js').then(utils => utils.handleCnpjInput(event.target));
    }
    if (event.target.classList.contains('campo-nome')) {
        import('./utils.js').then(utils => utils.handleNomeInput(event.target));
    }
    if (event.target.classList.contains('campo-numero')) {
        import('./utils.js').then(utils => utils.handleNumeroInput(event.target));
    }
});


// --- INÍCIO DO LISTENER ADICIONADO ---
// Novo listener, específico para o evento 'change', ideal para checkboxes e selects.
document.addEventListener('change', function (event) {
    // Procura por cliques no nosso toggle switch customizado.
    if (event.target.classList.contains('campo-ativo-toggle')) {
        // Importa o utils.js e chama a função para atualizar o campo oculto.
        import('./utils.js').then(utils => {
            if (utils.handleAtivoToggle) {
                utils.handleAtivoToggle(event.target);
            }
        });
    }
});
// --- FIM DO LISTENER ADICIONADO ---


// Listener global para 'submit', que envia os formulários dentro de modais via AJAX
document.addEventListener('submit', function (event) {
    const form = event.target;
    // Verifica se o formulário que está sendo enviado está dentro do nosso modal global
    if (form.closest('#modal-global')) {
        event.preventDefault(); // Impede o recarregamento da página
        import('./ui.js').then(ui => {
            fetch(form.action, {
                method: 'POST',
                body: new FormData(form)
            })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        // Em caso de sucesso, redireciona para a URL que o controller informar
                        window.location.href = data.url;
                    } else {
                        // Em caso de falha de validação, exibe os erros em um toast
                        ui.showToast('<ul>' + data.errors.map(e => `<li>${e}</li>`).join('') + '</ul>', 'error');
                    }
                })
                .catch(error => {
                    ui.showToast('Ocorreu um erro de comunicação com o servidor.', 'error');
                    console.error('Erro no envio do formulário AJAX:', error);
                });
        });
    }
});


// Função de navegação genérica, mantida como no seu original.
function navigateTo(event) {
    event.preventDefault();
    event.stopPropagation();
    const element = event.currentTarget;
    const redirectUrl = element.dataset.redirectUrl;
    if (redirectUrl) {
        window.location.href = redirectUrl;
    }
}