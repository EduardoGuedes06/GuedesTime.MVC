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

    import('./utils.js?v=' + Date.now()).then(utils => {
        if (typeof utils.initializeClearInputButtons === 'function') {
            utils.initializeClearInputButtons();
        }

       
    });


    if (typeof initializeDynamicFilters === 'function') {
        initializeDynamicFilters();
    }
});

document.addEventListener('click', function (event) {
    const modalTrigger = event.target.closest('[data-modal-url]');
    const modalClose = event.target.closest('[data-modal-close]');
    const link = event.target.closest('a');

    const deactivateTrigger = event.target.closest('.btn-deactivate');
    const confirmYes = event.target.closest('.btn-confirm-yes');
    const confirmNo = event.target.closest('.btn-confirm-no');
    if (deactivateTrigger || confirmNo || confirmYes) {
        event.preventDefault();

        if (deactivateTrigger) {
            const container = deactivateTrigger.closest('.inline-confirm-container');
            const popup = container.querySelector('.inline-confirm-popup');

            popup.classList.remove('popup-left', 'popup-right', 'popup-top', 'popup-bottom');
            const triggerRect = deactivateTrigger.getBoundingClientRect();
            const popupWidth = popup.offsetWidth;

            if (popup.classList.contains('popup-right-default') || triggerRect.left < popupWidth + 20) {
                popup.classList.add('popup-right');
            } else {
                popup.classList.add('popup-left');
            }

            document.querySelectorAll('.inline-confirm-popup.active').forEach(p => p.classList.remove('active'));
            popup.classList.add('active');
        }

        if (confirmNo) {
            confirmNo.closest('.inline-confirm-popup').classList.remove('active');
        }

        if (confirmYes) {
            const popup = confirmYes.closest('.inline-confirm-popup');
            const card = confirmYes.closest('.entity-card');
            const url = confirmYes.dataset.url;
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            if (!url || !token) {
                console.error('URL ou Token não encontrado para confirmação.');
                return;
            }

            loadingService.show("Processando...");
            popup.classList.remove('active');

            fetch(url, {
                method: 'POST',
                headers: { 'RequestVerificationToken': token }
            })
                .then(res => res.ok ? res.json() : res.json().then(err => Promise.reject(err)))
                .then(data => {
                    loadingService.hide();
                    import('./ui.js').then(ui => {
                        if (data.success) {
                            ui.showToast(data.message, 'success');
                            if (card) {
                                card.style.transition = 'opacity 0.5s ease';
                                card.style.opacity = '0';
                                setTimeout(() => card.remove(), 500);
                            }
                        } else {
                            ui.showToast(data.message || 'Ocorreu um erro.', 'error');
                        }
                    });
                })
                .catch(err => {
                    loadingService.hide();
                    import('./ui.js').then(ui => {
                        ui.showToast(err.message || 'Erro de comunicação.', 'error');
                    });
                });
        }
    }
    else if (modalTrigger) {
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

                    import('./utils.js?v=' + Date.now()).then(utils => {
                        const toggles = modalContent.querySelectorAll('input[class*="-toggle"]');
                        toggles.forEach(toggle => {
                            if (typeof utils.initializeToggleSwitch === 'function') {
                                utils.initializeToggleSwitch(toggle);
                            }
                        });
                    });
                })
                .catch(err => {
                    console.error('Erro ao carregar conteúdo do modal:', err);
                    modalContent.innerHTML = '<p style="text-align:center; padding: 20px; color: red;">Erro ao carregar conteúdo.</p>';
                });
        });
    }
    else if (modalClose) {
        import('./ui.js').then(ui => {
            const modalToClose = modalClose.closest('.modal-overlay');
            if (modalToClose) ui.closeModal(modalToClose);
        });
    }
    else if (link && link.href && !link.hasAttribute('data-no-loading') && link.target !== '_blank' && !link.href.startsWith('#')) {
        const isInternalLink = new URL(link.href).host === window.location.host;
        if (isInternalLink) {
            loadingService.show("Carregando página...");
        }
    }
    else if (!event.target.closest('.inline-confirm-popup')) {
        document.querySelectorAll('.inline-confirm-popup.active').forEach(p => p.classList.remove('active'));
    }
});

const classeParaFuncao = {
    'campo-cep': 'handleCepInput',
    'campo-cnpj': 'handleCnpjInput',
    'campo-nome': 'handleNomeInput',
    'campo-numero': 'handleNumeroInput',
    'campo-filtro': 'handleFiltroInput',
    'campo-ordinal-unico': 'handleOrdinalUnicoInput',
    'campo-ordinal-multiplo': 'handleOrdinalMultiploInput'
};

document.addEventListener('input', function (event) {
    const classe = Object.keys(classeParaFuncao).find(cl => event.target.classList.contains(cl));
    if (classe) {
        import('./utils.js?v=' + Date.now()).then(utils => {
            const func = utils[classeParaFuncao[classe]];
            if (typeof func === 'function') {
                func(event.target);
            } else {
                console.error(`⚠️ Função '${classeParaFuncao[classe]}' não encontrada no módulo.`, utils);
            }
        });
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
                        debugger
                        const toastType = data.type || 'error';
                        ui.showToast(
                            '<ul>' + data.errors.map(e => `<li>${e}</li>`).join('') + '</ul>',
                            toastType
                        );
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

function initializeDynamicFilters() {
    document.querySelectorAll('.js-dynamic-filter-form').forEach(form => {
        const url = form.dataset.url;
        const listContainerSelector = form.dataset.listContainer;
        const listContainer = document.querySelector(listContainerSelector);
        let searchTimeout;

        if (!url || !listContainer) {
            console.error('Formulário de filtro dinâmico não configurado corretamente.', form);
            return;
        }

        async function aplicarFiltros(page) {
            const formData = new FormData(form);
            if (!formData.has('ativo')) {
                formData.append('ativo', 'false');
            }
            const params = new URLSearchParams(formData);
            params.set('page', page || 1);
            listContainer.style.opacity = '0.5';
            try {
                const response = await fetch(`${url}?${params.toString()}`, {
                    method: 'GET',
                    headers: { 'X-Requested-With': 'XMLHttpRequest' }
                });
                if (!response.ok) throw new Error('Falha na resposta da rede.');
                const html = await response.text();
                listContainer.innerHTML = html;
            } catch (error) {
                console.error('Ocorreu um erro ao carregar os dados.', error);
                alert('Ocorreu um erro ao carregar os dados.');
            } finally {
                listContainer.style.opacity = '1';
            }
        }
        const searchInput = form.querySelector('.js-dynamic-search');
        if (searchInput) {
            searchInput.addEventListener('keyup', () => {
                clearTimeout(searchTimeout);
                searchTimeout = setTimeout(() => aplicarFiltros(1), 500);
            });
        }
        form.querySelectorAll('.js-dynamic-filter').forEach(filter => {
            filter.addEventListener('change', () => aplicarFiltros(1));
        });
        const clearButton = form.querySelector('.js-clear-filters-btn');
        if (clearButton) {
            clearButton.addEventListener('click', () => {
                form.reset();
                form.querySelectorAll('.js-default-checked').forEach(cb => cb.checked = true);
                aplicarFiltros(1);
            });
        }
        document.addEventListener('click', event => {
            const button = event.target.closest('.pagination-controls button');
            if (button && button.dataset.page && !button.disabled) {
                event.preventDefault();
                aplicarFiltros(button.dataset.page);
            }
        });
    });
}