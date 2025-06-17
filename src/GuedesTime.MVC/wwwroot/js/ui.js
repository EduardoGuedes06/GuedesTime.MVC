
const toastContainer = document.getElementById('toast-container');
export function showToast(message, type = 'success') {
    debugger
    const toastContainer = document.getElementById('toast-container');
    if (!toastContainer || !message) return;

    const toast = document.createElement('div');
    toast.className = `toast ${type}`;

    let icon = '';
    if (type === 'success') icon = '<i class="fas fa-check-circle"></i>';
    else if (type === 'error') icon = '<i class="fas fa-times-circle"></i>';
    else if (type === 'warning') icon = '<i class="fas fa-exclamation-triangle"></i>';
    else if (type === 'info') icon = '<i class="fas fa-info-circle"></i>';

    toast.innerHTML = `${icon}<span>${message}</span>`;
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

export function setupMultiSelect(containerId, selectedContainerId, searchInputId, availableListId, allItems, itemPropertyId, itemPropertyName, selectedIdsArrayRef) {
    const selectedContainer = document.getElementById(selectedContainerId);
    const searchInput = document.getElementById(searchInputId);
    const availableList = document.getElementById(availableListId);
    const parentContainer = document.getElementById(containerId);

    if (!selectedContainer || !searchInput || !availableList || !parentContainer) {
        console.error('MultiSelect Error: One or more element IDs are invalid.');
        return;
    }

    let currentSelectedIds = new Set(selectedIdsArrayRef);

    const renderSelected = () => {
        selectedContainer.innerHTML = '';
        Array.from(currentSelectedIds).forEach(id => {
            const item = allItems.find(i => i[itemPropertyId] === id);
            if (item) {
                const chip = document.createElement('span');
                chip.className = 'selected-item-chip';
                chip.innerHTML = `${item[itemPropertyName]} <button type="button" class="remove-btn" data-id="${id}"><i class="fas fa-times"></i></button>`;
                selectedContainer.appendChild(chip);

                chip.querySelector('.remove-btn').addEventListener('click', (e) => {
                    const removeId = e.currentTarget.dataset.id;
                    currentSelectedIds.delete(removeId);
                    const index = selectedIdsArrayRef.indexOf(removeId);
                    if (index > -1) {
                        selectedIdsArrayRef.splice(index, 1);
                    }
                    renderSelected();
                    renderAvailable(searchInput.value);
                });
            }
        });
    };

    const renderAvailable = (searchTerm = '') => {
        availableList.innerHTML = '';
        const filteredItems = allItems.filter(item =>
            !currentSelectedIds.has(item[itemPropertyId]) &&
            item[itemPropertyName].toLowerCase().includes(searchTerm.toLowerCase())
        );

        if (filteredItems.length === 0) {
            availableList.innerHTML = '<div class="available-item text-center">Nenhum item disponível.</div>';
        }

        filteredItems.forEach(item => {
            const listItem = document.createElement('div');
            listItem.className = 'available-item';
            listItem.textContent = item[itemPropertyName];
            listItem.dataset.id = item[itemPropertyId];
            listItem.addEventListener('click', () => {
                currentSelectedIds.add(item[itemPropertyId]);
                if (!selectedIdsArrayRef.includes(item[itemPropertyId])) {
                    selectedIdsArrayRef.push(item[itemPropertyId]);
                }
                searchInput.value = '';
                renderSelected();
                renderAvailable('');
                availableList.classList.remove('active');
            });
            availableList.appendChild(listItem);
        });

        if (filteredItems.length > 0 || searchTerm !== '') {
            availableList.classList.add('active');
        } else {
            availableList.classList.remove('active');
        }
    };

    searchInput.addEventListener('input', () => renderAvailable(searchInput.value));
    searchInput.addEventListener('focus', () => renderAvailable(searchInput.value));

    document.addEventListener('click', (e) => {
        if (!parentContainer.contains(e.target)) {
            availableList.classList.remove('active');
        }
    });

    renderSelected();
    renderAvailable('');

    return {
        getSelectedIds: () => Array.from(currentSelectedIds),
        setSelectedIds: (ids) => {
            currentSelectedIds = new Set(ids);
            selectedIdsArrayRef.length = 0;
            ids.forEach(id => selectedIdsArrayRef.push(id));
            renderSelected();
            renderAvailable(searchInput.value);
        },
        clear: () => {
            currentSelectedIds.clear();
            selectedIdsArrayRef.length = 0;
            renderSelected();
            renderAvailable('');
        }
    };
}