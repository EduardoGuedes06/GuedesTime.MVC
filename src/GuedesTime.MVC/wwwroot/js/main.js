import './services/fetchInterceptor.js';
import { loadingService } from './services/loadingService.js';

document.addEventListener('DOMContentLoaded', () => {
    import('./ui.js').then(ui => {
        const successMessage = document.getElementById('tempdata-success-message')?.value;
        if (successMessage) ui.showToast(successMessage, 'success');
        const errorMessage = document.getElementById('tempdata-error-message')?.value;
        if (errorMessage) ui.showToast(errorMessage, 'error');
    });
    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebar-toggle');
    const mainContent = document.querySelector('.main-content');
    function handleSidebarDisplay() {
        if (!sidebar || !mainContent) return;
        if (window.innerWidth >= 768) {
            sidebar.classList.add('active');
            mainContent.classList.add('expanded');
        } else {
            sidebar.classList.remove('active');
            mainContent.classList.remove('expanded');
        }
    }
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', () => {
            sidebar.classList.toggle('active');
            mainContent.classList.toggle('expanded');
        });
    }
    window.addEventListener('resize', handleSidebarDisplay);
    handleSidebarDisplay();
});

document.addEventListener('click', (event) => {
    const modalTrigger = event.target.closest('[data-modal-url]');
    const closeTrigger = event.target.closest('[data-modal-close]');
    const link = event.target.closest('a');

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
                    modalContent.innerHTML = html;
                    import('./utils.js').then(utils => {
                        const allToggles = modalContent.querySelectorAll('input[class*="-toggle"]');
                        if (allToggles.length > 0 && utils.initializeToggleSwitch) {
                            allToggles.forEach(toggle => {
                                if (Array.from(toggle.classList).some(cls => cls.startsWith('campo-') && cls.endsWith('-toggle'))) {
                                    utils.initializeToggleSwitch(toggle);
                                }
                            });
                        }
                    });
                })
                .catch(err => {
                    console.error('Erro ao carregar conteúdo do modal:', err);
                    modalContent.innerHTML = '<p style="text-align:center; padding: 20px; color: red;">Erro ao carregar conteúdo.</p>';
                });
        });
    }
    else if (closeTrigger) {
        import('./ui.js').then(ui => {
            const modalToClose = closeTrigger.closest('.modal-overlay');
            if (modalToClose) ui.closeModal(modalToClose);
        });
    }
    else if (link && link.href && !link.hasAttribute('data-no-loading') && link.target !== '_blank' && !link.href.startsWith('#')) {
        const isInternalLink = new URL(link.href).host === window.location.host;
        if (isInternalLink) {
            loadingService.show("Carregando p\u00E1gina...");
        }
    }
});

document.addEventListener('input', (event) => {
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

document.addEventListener('submit', function (event) {
    const form = event.target;
    if (form.closest('#modal-global')) {
        event.preventDefault();
        import('./ui.js').then(ui => {
            fetch(form.action, {
                method: 'POST',
                body: new FormData(form)
            })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        window.location.href = data.url;
                    } else {
                        ui.showToast('<ul>' + data.errors.map(e => `<li>${e}</li>`).join('') + '</ul>', 'error');
                    }
                })
                .catch(error => {
                    ui.showToast('Ocorreu um erro de comunicação com o servidor.', 'error');
                    console.error('Erro no envio do formulário AJAX:', error);
                });
        });
    }
    else if (!form.hasAttribute('data-no-loading')) {
        loadingService.show("Processando...");
    }
});

window.addEventListener('pageshow', (event) => {
    if (event.persisted) {
        loadingService.hide();
    }
});

function navigateTo(event) {
    event.preventDefault();
    event.stopPropagation();
    const element = event.currentTarget;
    const redirectUrl = element.dataset.redirectUrl;
    if (redirectUrl) {
        window.location.href = redirectUrl;
    }
}
window.navigateTo = navigateTo;