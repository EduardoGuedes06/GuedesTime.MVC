import { appData } from '../state.js';
import { showToast, openModal, closeModal } from '../ui.js';

// Todos os 'getElementById' de botões foram removidos daqui.

export function updateSidebarVisibility() {
    for (const moduleName in appData.modules) {
        const navLink = document.querySelector(`.sidebar-nav a[data-section="${moduleName}"]`);
        if (navLink) {
            navLink.parentElement.style.display = appData.modules[moduleName] ? '' : 'none';
        }
    }
}

function openModalHorarios() {
    const inputStartTime = document.getElementById('config-start-time');
    const inputEndTime = document.getElementById('config-end-time');
    inputStartTime.value = appData.settings.startTime;
    inputEndTime.value = appData.settings.endTime;
    openModal(document.getElementById('modal-horarios'));
}

function saveSettingsHorarios(e) {
    e.preventDefault();
    const inputStartTime = document.getElementById('config-start-time');
    const inputEndTime = document.getElementById('config-end-time');
    appData.settings.startTime = inputStartTime.value;
    appData.settings.endTime = inputEndTime.value;
    showToast('Horários padrão salvos com sucesso!');
    closeModal(document.getElementById('modal-horarios'));
}

function openModalNotificacoes() {
    const inputEmailTemplate = document.getElementById('config-email-template');
    const inputEnableEmail = document.getElementById('inputEnableEmail');
    inputEmailTemplate.value = appData.settings.emailTemplate;
    inputEnableEmail.checked = appData.settings.enableEmail;
    openModal(document.getElementById('modal-notificacoes'));
}

function saveSettingsNotificacoes(e) {
    e.preventDefault();
    const inputEmailTemplate = document.getElementById('config-email-template');
    const inputEnableEmail = document.getElementById('inputEnableEmail');
    appData.settings.emailTemplate = inputEmailTemplate.value;
    appData.settings.enableEmail = inputEnableEmail.checked;
    showToast('Configurações de notificação salvas com sucesso!');
    closeModal(document.getElementById('modal-notificacoes'));
}

function showTab(tabId) {
    document.querySelectorAll('.modal-tab-content').forEach(content => content.classList.remove('active'));
    document.querySelectorAll('.modal-tab-link').forEach(link => link.classList.remove('active'));
    document.getElementById(`tab-${tabId}`).classList.add('active');
    document.querySelector(`.modal-tab-link[data-tab="${tabId}"]`).classList.add('active');
}

function openModalAparencia() {
    for (const [variable, color] of Object.entries(appData.settings.theme.colors)) {
        const picker = document.querySelector(`[data-variable="${variable}"]`);
        if (picker) {
            picker.value = color;
        }
    }
    document.querySelectorAll('.theme-card').forEach(card => {
        card.classList.remove('active-theme');
        if (card.dataset.themeName === appData.settings.theme.name) {
            card.classList.add('active-theme');
        }
    });
    showTab('temas');
    openModal(document.getElementById('modal-aparencia'));
}

function saveSettingsAparencia(e) {
    e.preventDefault();
    showToast('Aparência salva com sucesso!');
    closeModal(document.getElementById('modal-aparencia'));
}

function openModalGerais() {
    document.getElementById('config-max-versions').value = appData.settings.maxVersions;
    document.getElementById('config-allow-teacher-availability').checked = appData.settings.allowTeacherAvailability;
    document.getElementById('config-auto-resolve-conflicts').checked = appData.settings.autoResolveConflicts;
    document.getElementById('config-import-holidays').checked = appData.settings.importHolidays;
    document.getElementById('config-enable-substitutions').checked = appData.settings.enableSubstitutions;
    document.getElementById('config-enforce-capacity').checked = appData.settings.enforceCapacity;
    document.getElementById('config-notify-on-change').checked = appData.settings.notifyOnChange;
    document.getElementById('config-enable-teacher-review').checked = appData.settings.enableTeacherReview;
    document.getElementById('config-allow-weekend').checked = appData.settings.allowWeekend;
    document.getElementById('config-maintenance-mode').checked = appData.settings.maintenanceMode;
    document.getElementById('config-suggest-template').checked = appData.settings.suggestTemplate;
    openModal(document.getElementById('modal-gerais'));
}

function saveSettingsGerais(e) {
    e.preventDefault();
    appData.settings.maxVersions = parseInt(document.getElementById('config-max-versions').value, 10);
    appData.settings.allowTeacherAvailability = document.getElementById('config-allow-teacher-availability').checked;
    appData.settings.autoResolveConflicts = document.getElementById('config-auto-resolve-conflicts').checked;
    appData.settings.importHolidays = document.getElementById('config-import-holidays').checked;
    appData.settings.enableSubstitutions = document.getElementById('config-enable-substitutions').checked;
    appData.settings.enforceCapacity = document.getElementById('config-enforce-capacity').checked;
    appData.settings.notifyOnChange = document.getElementById('config-notify-on-change').checked;
    appData.settings.enableTeacherReview = document.getElementById('config-enable-teacher-review').checked;
    appData.settings.allowWeekend = document.getElementById('config-allow-weekend').checked;
    appData.settings.maintenanceMode = document.getElementById('config-maintenance-mode').checked;
    appData.settings.suggestTemplate = document.getElementById('config-suggest-template').checked;
    showToast('Configurações gerais salvas com sucesso!');
    closeModal(document.getElementById('modal-gerais'));
}

function openModalModulos() {
    const moduleToggles = { /* ... */ }; // A definição já está fora, mas seria ideal estar aqui
    for (const moduleName in moduleToggles) {
        const toggle = document.getElementById(`module-${moduleName}`);
        if (toggle) {
            toggle.checked = appData.modules[moduleName];
        }
    }
    openModal(document.getElementById('modal-modulos'));
}

function saveSettingsModulos(e) {
    e.preventDefault();
    const moduleNames = ['teachers', 'subjects', 'series', 'classes', 'classrooms', 'timeSlots', 'holidays', 'lessonPlans', 'teacherReview', 'settings'];
    moduleNames.forEach(moduleName => {
        const toggle = document.getElementById(`module-${moduleName}`);
        if (toggle) {
            appData.modules[moduleName] = toggle.checked;
        }
    });
    updateSidebarVisibility();
    showToast('Visibilidade dos módulos atualizada!');
    closeModal(document.getElementById('modal-modulos'));
}

function openPlaceholderModal(title, text) {
    document.getElementById('placeholder-modal-title').textContent = title;
    document.getElementById('placeholder-modal-text').textContent = text;
    openModal(document.getElementById('modal-placeholder'));
}

export function initSettings() {
    // A verificação principal foi removida daqui para verificarmos cada botão individualmente.
    
    // Botões de configuração
    const btnHorarios = document.getElementById('btn-config-horarios');
    if (btnHorarios) btnHorarios.addEventListener('click', openModalHorarios);
    
    const btnNotificacoes = document.getElementById('btn-config-notificacoes');
    if (btnNotificacoes) btnNotificacoes.addEventListener('click', openModalNotificacoes);
    
    const btnAparencia = document.getElementById('btn-config-aparencia');
    if (btnAparencia) btnAparencia.addEventListener('click', openModalAparencia);
    
    const btnGerais = document.getElementById('btn-config-gerais');
    if (btnGerais) btnGerais.addEventListener('click', openModalGerais);

    const btnModulos = document.getElementById('btn-config-modulos');
    if (btnModulos) btnModulos.addEventListener('click', openModalModulos);

    // Formulários dos modais
    const formHorarios = document.getElementById('form-horarios');
    if (formHorarios) formHorarios.addEventListener('submit', saveSettingsHorarios);

    const formNotificacoes = document.getElementById('form-notificacoes');
    if (formNotificacoes) formNotificacoes.addEventListener('submit', saveSettingsNotificacoes);
    
    const formAparencia = document.getElementById('form-aparencia');
    if (formAparencia) formAparencia.addEventListener('submit', saveSettingsAparencia);

    const formGerais = document.getElementById('form-gerais');
    if (formGerais) formGerais.addEventListener('submit', saveSettingsGerais);

    const formModulos = document.getElementById('form-modulos');
    if (formModulos) formModulos.addEventListener('submit', saveSettingsModulos);

    // Abas do modal de aparência
    document.querySelectorAll('.modal-tab-link').forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            showTab(e.target.dataset.tab);
        });
    });
    
    // Botões de cancelamento dos modais
    document.querySelectorAll('.btn-modal-cancel').forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal-overlay');
            if (modal) closeModal(modal);
        });
    });

    const btnFuncoes = document.getElementById('btn-config-funcoes');
    if (btnFuncoes) btnFuncoes.addEventListener('click', () => openPlaceholderModal('Funções e Permissões', 'Área para gerenciar perfis (Ex: Administrador, Coordenador) e definir quais seções cada um pode acessar.'));
    
    const btnIntegracoes = document.getElementById('btn-config-integracoes');
    if(btnIntegracoes) btnIntegracoes.addEventListener('click', () => openPlaceholderModal('Integrações', 'Área para configurar conexões com outras plataformas (Ex: Google Agenda, Slack).'));

    const btnBackup = document.getElementById('btn-config-backup');
    if(btnBackup) btnBackup.addEventListener('click', () => openPlaceholderModal('Backup e Exportação', 'Opções para gerar e baixar backups dos dados da instituição.'));

    const btnRegiao = document.getElementById('btn-config-regiao');
    if(btnRegiao) btnRegiao.addEventListener('click', () => openPlaceholderModal('Formato de Datas e Região', 'Configurações de fuso horário, idioma e formato de exibição de datas.'));

    const btnSeguranca = document.getElementById('btn-config-seguranca');
    if(btnSeguranca) btnSeguranca.addEventListener('click', () => openPlaceholderModal('Segurança', 'Definição de políticas de senha, tempo de sessão e autenticação de dois fatores (2FA).'));

    const btnApi = document.getElementById('btn-config-api');
    if(btnApi) btnApi.addEventListener('click', () => openPlaceholderModal('API e Webhooks', 'Área para gerar e gerenciar chaves de API para integrações personalizadas.'));

    const btnTermos = document.getElementById('btn-config-termos');
    if(btnTermos) btnTermos.addEventListener('click', () => openPlaceholderModal('Termos e Privacidade', 'Campos para inserir os links para as páginas de Termos de Uso e Política de Privacidade.'));

    updateSidebarVisibility();
}