import { openModal, closeModal } from '../ui.js';

function normalizeText(s) {
    return (s || '')
        .normalize('NFD')
        .replace(/\p{Diacritic}/gu, '')
        .toLowerCase();
}

export function openSuggestionPickerModal({
    title,
    items,
    selectedItems,
    maxItemsToShow = 80,
    maxSelected = 30
}) {
    const all = (items || []).filter(Boolean);
    const selected = new Set((selectedItems || []).filter(Boolean));

    const overlay = document.createElement('div');
    overlay.className = 'modal-overlay active suggestion-picker-overlay';
    overlay.setAttribute('role', 'dialog');
    overlay.setAttribute('aria-modal', 'true');

    const content = document.createElement('div');
    content.className = 'modal-content large-modal suggestion-picker-content';

    content.innerHTML = `
        <div class="modal-title-container">
            <h3 class="modal-title">${title || 'Sugestões'}</h3>
            <button type="button" class="btn secondary-btn" data-sp-close>Fechar</button>
        </div>
        <div class="form-group" style="margin-bottom: 12px;">
            <label class="control-label">Buscar</label>
            <input type="text" class="form-control" placeholder="Digite para filtrar..." data-sp-search />
            <small class="text-muted">Limite: ${maxSelected} selecionados. Mostrando até ${maxItemsToShow} itens filtrados.</small>
        </div>

        <div class="suggestion-picker-actions">
            <button type="button" class="btn secondary-btn" data-sp-select-all>Selecionar tudo (visível)</button>
            <button type="button" class="btn secondary-btn" data-sp-clear>Limpar seleção</button>
            <div style="margin-left:auto; color: var(--petroleum-blue); font-weight: 700;">
                <span data-sp-count>0</span> selecionados
            </div>
        </div>

        <div class="suggestion-picker-list" data-sp-list></div>

        <div class="modal-actions" style="margin-top: 18px;">
            <button type="button" class="btn secondary-btn" data-sp-close>Cancelar</button>
            <button type="button" class="btn primary-btn" data-sp-apply>Inserir selecionados</button>
        </div>
    `;

    overlay.appendChild(content);
    document.body.appendChild(overlay);
    openModal(overlay);

    const elSearch = content.querySelector('[data-sp-search]');
    const elList = content.querySelector('[data-sp-list]');
    const elCount = content.querySelector('[data-sp-count]');
    const btnClose = content.querySelectorAll('[data-sp-close]');
    const btnApply = content.querySelector('[data-sp-apply]');
    const btnSelectAll = content.querySelector('[data-sp-select-all]');
    const btnClear = content.querySelector('[data-sp-clear]');

    const updateCount = () => {
        if (elCount) elCount.textContent = String(selected.size);
    };

    const render = () => {
        const q = normalizeText(elSearch?.value || '');
        const filtered = q
            ? all.filter(x => normalizeText(x).includes(q))
            : all.slice();

        const limited = filtered.slice(0, maxItemsToShow);
        elList.innerHTML = '';

        limited.forEach((name) => {
            const row = document.createElement('label');
            row.className = 'suggestion-picker-row';
            const checked = selected.has(name);
            row.innerHTML = `
                <input type="checkbox" ${checked ? 'checked' : ''} />
                <span>${name}</span>
            `;
            const cb = row.querySelector('input');
            cb.addEventListener('change', () => {
                if (cb.checked) {
                    if (selected.size >= maxSelected) {
                        cb.checked = false;
                        return;
                    }
                    selected.add(name);
                } else {
                    selected.delete(name);
                }
                updateCount();
            });
            elList.appendChild(row);
        });

        updateCount();
        return limited;
    };

    let lastVisible = render();

    elSearch?.addEventListener('input', () => {
        lastVisible = render();
    });

    btnSelectAll?.addEventListener('click', () => {
        for (const name of lastVisible) {
            if (selected.size >= maxSelected) break;
            selected.add(name);
        }
        lastVisible = render();
    });

    btnClear?.addEventListener('click', () => {
        selected.clear();
        lastVisible = render();
    });

    const cleanup = () => {
        closeModal(overlay);
        overlay.remove();
        document.removeEventListener('keydown', onKeyDown);
    };

    const onKeyDown = (ev) => {
        if (ev.key === 'Escape') cleanup();
    };
    document.addEventListener('keydown', onKeyDown);

    btnClose.forEach(b => b.addEventListener('click', cleanup));

    overlay.addEventListener('click', (ev) => {
        if (ev.target === overlay) cleanup();
    });

    let resolved = false;
    return new Promise((resolve) => {
        btnApply?.addEventListener('click', () => {
            resolved = true;
            const result = Array.from(selected);
            cleanup();
            resolve(result);
        });

        // Cancel resolve
        const originalCleanup = cleanup;
        const cleanupResolveCancel = () => {
            originalCleanup();
            if (!resolved) resolve(null);
        };
        btnClose.forEach(b => b.addEventListener('click', cleanupResolveCancel));
        overlay.addEventListener('click', (ev) => {
            if (ev.target === overlay) cleanupResolveCancel();
        });
        document.addEventListener('keydown', (ev) => {
            if (ev.key === 'Escape') cleanupResolveCancel();
        });

        setTimeout(() => elSearch?.focus(), 50);
    });
}

