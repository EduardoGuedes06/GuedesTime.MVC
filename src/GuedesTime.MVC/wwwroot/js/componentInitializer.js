async function initializeMultiSelect(container) {
    const ui = await import('./ui.js');
    const config = container.dataset;
    const formGroup = container.closest('.form-group');
    const hiddenInput = formGroup ? formGroup.querySelector('input[name="DisciplinaIds"]') : null;

    if (!hiddenInput) {
        console.error('MultiSelect: Input hidden com name="DisciplinaIds" não foi encontrado dentro do .form-group pai.', container);
        return;
    }

    let initialItems = [];
    try {
        if (config.initialItems) {
            initialItems = JSON.parse(config.initialItems);
        }
    } catch (e) {
        console.error('Erro ao parsear initial-items do MultiSelect', e);
    }

    let selectedIds = [];
    try {
        if (hiddenInput.value) {
            selectedIds = JSON.parse(hiddenInput.value);
        }
    } catch (e) {
        console.error('Erro ao parsear os IDs de disciplinas selecionadas:', e);
    }

    ui.setupMultiSelect({
        containerId: container.id,
        selectedContainerId: config.selectedContainerId,
        searchInputId: config.searchInputId,
        availableListId: config.availableListId,
        itemPropertyId: config.itemIdProp,
        itemPropertyName: config.itemNameProp,
        searchEndpoint: config.searchEndpoint,
        selectedIdsArrayRef: selectedIds,
        hiddenInputToUpdate: hiddenInput,
        initialItems: initialItems
    });
}

async function initializeToggle(toggle) {
    const utils = await import('./utils.js');
    if (typeof utils.initializeToggleSwitch === 'function') {
        utils.initializeToggleSwitch(toggle);
    }
}

async function initializeClearButtons() {
    const utils = await import('./utils.js');
    if (typeof utils.initializeClearInputButtons === 'function') {
        if (!window.clearInputButtonsInitialized) {
            utils.initializeClearInputButtons();
            window.clearInputButtonsInitialized = true;
        }
    }
}

async function initializeUserAvatarUploader(container) {
    const mod = await import('./pages/usuarioAvatar.js');
    if (typeof mod.initializeUserAvatarUploader === 'function') {
        mod.initializeUserAvatarUploader(container);
    }
}

async function initializeDisciplinaNomesHelper(container) {
    const mod = await import('./pages/disciplinaNomes.js');
    if (typeof mod.initializeDisciplinaNomesHelper === 'function') {
        mod.initializeDisciplinaNomesHelper(container);
    }
}

const componentMap = {
    '[data-component="multi-select"]': initializeMultiSelect,
    'input[class*="-toggle"]': initializeToggle,
    '[data-component="user-avatar-uploader"]': initializeUserAvatarUploader,
    '[data-component="disciplina-nomes-helper"]': initializeDisciplinaNomesHelper
};

export function initializeComponents(container) {
    console.log('Inicializando componentes dinâmicos em:', container);
    for (const selector in componentMap) {
        const elements = container.querySelectorAll(selector);
        if (elements.length > 0) {
            console.log(`Encontrado(s) ${elements.length} elemento(s) para o seletor: ${selector}`);
            elements.forEach(element => {
                componentMap[selector](element);
            });
        }
    }
    initializeClearButtons();
}