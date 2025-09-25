export function showToast(message, type = 'success') {
    const toastContainer = document.getElementById('toast-container');
    if (!toastContainer || !message) return;

    const toast = document.createElement('div');
    toast.className = `toast ${type}`;

    let icon = '';
    if (type === 'success') icon = '<i class="fas fa-check-circle"></i>';
    else if (type === 'error') icon = '<i class="fas fa-times-circle"></i>';
    else if (type === 'warning') icon = '<i class="fas fa-exclamation-triangle"></i>';
    else if (type === 'info') icon = '<i class="fas fa-info-circle"></i>';

    const isMultiline = message.includes('<ul>') || message.length > 100;

    if (isMultiline) {
        toast.innerHTML = `
            <div class="toast-content">
                <div class="toast-icon">${icon}</div>
                <div class="toast-message">${message}</div>
            </div>
        `;
    } else {
        toast.innerHTML = `<span class="toast-icon">${icon}</span>${message}`;
    }
    toastContainer.appendChild(toast);

    setTimeout(() => toast.classList.add('show'), 100);
    setTimeout(() => {
        toast.classList.remove('show');
        toast.addEventListener('transitionend', () => toast.remove());
    }, 5000);
}

export function openModal(modalElement) {
    if (!modalElement) return;
    modalElement.classList.add('active');
    document.body.style.overflow = 'hidden';
}

export function closeModal(modalElement) {
    if (!modalElement) return;
    modalElement.classList.remove('active');
    document.body.style.overflow = '';
}

export function renderPagination(containerId, totalPages, currentPage, renderFunction) {
    const paginationContainer = document.getElementById(containerId);
    if (!paginationContainer) return;
    paginationContainer.innerHTML = '';
    if (totalPages <= 1) return;

    const createButton = (text, pageNum, isActive = false, isDisabled = false) => {
        const button = document.createElement('button');
        button.innerHTML = text;
        button.className = `pagination-btn ${isActive ? 'active' : ''}`;
        if (isDisabled) {
            button.disabled = true;
        } else {
            button.addEventListener('click', () => renderFunction(pageNum));
        }
        return button;
    };

    paginationContainer.appendChild(createButton('&laquo; Previous', currentPage - 1, false, currentPage === 1));
    for (let i = 1; i <= totalPages; i++) {
        paginationContainer.appendChild(createButton(i, i, i === currentPage));
    }
    paginationContainer.appendChild(createButton('Next &raquo;', currentPage + 1, false, currentPage === totalPages));
}

export function setupMultiSelect(config) {
    const {
        containerId, selectedContainerId, searchInputId, availableListId,
        itemPropertyId, itemPropertyName, selectedIdsArrayRef, searchEndpoint,
        hiddenInputToUpdate
    } = config;

    const selectedContainer = document.getElementById(selectedContainerId);
    const searchInput = document.getElementById(searchInputId);
    const availableList = document.getElementById(availableListId);
    const parentContainer = document.getElementById(containerId);

    if (!selectedContainer || !searchInput || !availableList || !parentContainer || !hiddenInputToUpdate) {
        console.error('MultiSelect Error: Um ou mais elementos (ou o hiddenInput) são inválidos.', config);
        return;
    }

    let allItems = [];
    let currentSelectedIds = new Set(selectedIdsArrayRef);

    // NOVO: Criamos um cache para guardar os dados dos itens (ID -> {id, name})
    const itemCache = new Map();

    const updateHiddenInput = () => {
        hiddenInputToUpdate.value = JSON.stringify(Array.from(currentSelectedIds));
    };

    const renderSelected = () => {
        selectedContainer.innerHTML = '';
        currentSelectedIds.forEach(id => {
            // MUDANÇA: Buscamos primeiro no nosso cache
            const item = itemCache.get(id);
            const itemName = item ? item[itemPropertyName] : `Item ID: ${id}`;

            const chip = document.createElement('span');
            chip.className = 'selected-item-chip';
            chip.innerHTML = `${itemName} <button type="button" class="remove-btn" data-id="${id}"><i class="fas fa-times"></i></button>`;
            selectedContainer.appendChild(chip);

            chip.querySelector('.remove-btn').addEventListener('click', (e) => {
                const removeId = e.currentTarget.dataset.id;
                currentSelectedIds.delete(removeId);
                updateHiddenInput();
                renderSelected();
                renderAvailable(searchInput.value);
            });
        });
        updateHiddenInput();
    };

    const renderAvailable = (searchTerm = '') => {
        availableList.innerHTML = '';

        const filteredItems = allItems.filter(item =>
            !currentSelectedIds.has(item[itemPropertyId]) &&
            item[itemPropertyName].toLowerCase().includes(searchTerm.toLowerCase())
        );

        if (filteredItems.length === 0 && searchTerm) {
            availableList.innerHTML = '<div class="available-item text-center">Nenhum resultado encontrado.</div>';
        }

        filteredItems.forEach(item => {
            const listItem = document.createElement('div');
            listItem.className = 'available-item';
            listItem.textContent = item[itemPropertyName];
            listItem.dataset.id = item[itemPropertyId];
            listItem.addEventListener('click', () => {
                // Adicionamos o item completo ao cache ao selecionar
                if (!itemCache.has(item[itemPropertyId])) {
                    itemCache.set(item[itemPropertyId], item);
                }
                currentSelectedIds.add(item[itemPropertyId]);
                searchInput.value = '';
                renderSelected();
                renderAvailable('');
                availableList.classList.remove('active');
            });
            availableList.appendChild(listItem);
        });

        if (availableList.innerHTML !== '') {
            availableList.classList.add('active');
        } else {
            availableList.classList.remove('active');
        }
    };

    let searchTimeout;
    const searchRemote = async (query) => {
        if (!searchEndpoint) return;

        availableList.innerHTML = '<div class="available-item text-center">Buscando...</div>';
        availableList.classList.add('active');

        try {
            const res = await fetch(`${searchEndpoint}?query=${encodeURIComponent(query)}`);
            if (!res.ok) throw new Error('Erro ao buscar dados');
            const data = await res.json();

            // MUDANÇA: Em vez de apenas sobrescrever, também populamos o cache
            data.forEach(item => {
                if (!itemCache.has(item.id)) {
                    itemCache.set(item.id, item);
                }
            });

            allItems = data; // A lista de "todos os itens" ainda representa apenas a última busca
            renderAvailable(query);
        } catch (err) {
            console.error('Erro no MultiSelect remoto:', err);
            availableList.innerHTML = '<div class="available-item text-center text-danger">Erro ao buscar.</div>';
        }
    };

    searchInput.addEventListener('input', () => {
        clearTimeout(searchTimeout);
        const query = searchInput.value.trim();
        if (query.length < 2 && query.length > 0) {
            availableList.innerHTML = '<div class="available-item text-center">Digite ao menos 2 caracteres.</div>';
            availableList.classList.add('active');
            return;
        }
        if (query.length === 0) {
            availableList.classList.remove('active');
            return;
        }
        searchTimeout = setTimeout(() => searchRemote(query), 500);
    });

    searchInput.addEventListener('focus', () => {
        if (searchInput.value.trim().length > 1) {
            searchRemote(searchInput.value.trim());
        }
    });

    document.addEventListener('click', (e) => {
        if (!parentContainer.contains(e.target)) {
            availableList.classList.remove('active');
        }
    });

    renderSelected();
}