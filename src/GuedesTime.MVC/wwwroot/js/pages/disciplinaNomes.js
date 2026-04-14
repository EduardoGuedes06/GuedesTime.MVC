import { DISCIPLINAS_SUGESTOES } from '../data/disciplinasSugestoes.js';
import { openSuggestionPickerModal } from '../components/suggestionPickerModal.js';

function normalizeText(s) {
    return (s || '')
        .normalize('NFD')
        .replace(/\p{Diacritic}/gu, '')
        .toLowerCase();
}

function titleCasePtBr(text) {
    const t = (text || '').trim().replace(/\s+/g, ' ');
    if (!t) return '';
    return t
        .toLowerCase()
        .replace(/(^|[\s.])([a-zà-öø-ÿ])/g, (match, sep, char) => sep + char.toUpperCase());
}

function splitItems(value) {
    return (value || '')
        .split(',')
        .map(s => s.trim())
        .filter(Boolean);
}

function getActiveToken(value, caretPos) {
    const before = (value || '').slice(0, caretPos);
    const lastComma = before.lastIndexOf(',');
    const token = (lastComma >= 0 ? before.slice(lastComma + 1) : before).trim();
    const prefix = lastComma >= 0 ? (value.slice(0, lastComma + 1)) : '';
    const suffix = value.slice(prefix.length + (before.length - prefix.length));
    return { token, prefix };
}

function renderSuggestions(listEl, items, onPick) {
    listEl.innerHTML = '';
    if (items.length === 0) {
        listEl.style.display = 'none';
        return;
    }

    items.forEach((name, idx) => {
        const btn = document.createElement('button');
        btn.type = 'button';
        btn.className = 'disciplina-suggestion-item';
        btn.dataset.index = String(idx);
        btn.textContent = name;
        btn.addEventListener('click', () => onPick(name));
        listEl.appendChild(btn);
    });
    listEl.style.display = '';
}

export function initializeDisciplinaNomesHelper(container) {
    const textarea = container.querySelector('textarea[data-disciplina-nomes]');
    const panelEl = container.querySelector('[data-disciplina-suggestions]');
    const listEl = container.querySelector('[data-disciplina-suggestions-list]');
    const btnInsertAll = container.querySelector('[data-disciplina-insert-all]');
    const btnOpenAll = container.querySelector('[data-disciplina-open-all]');
    const previewEl = container.querySelector('[data-disciplina-preview]');
    if (!textarea || !panelEl || !listEl) return;

    const all = [...DISCIPLINAS_SUGESTOES].sort((a, b) => a.localeCompare(b, 'pt-BR'));
    const normalizedAll = all.map(a => ({ name: a, n: normalizeText(a) }));

    const hide = () => { panelEl.style.display = 'none'; };

    const applyFormatting = () => {
        const items = splitItems(textarea.value).map(titleCasePtBr);
        textarea.value = items.join(', ');
    };

    const updatePreview = () => {
        if (!previewEl) return;
        const items = splitItems(textarea.value).map(titleCasePtBr);
        if (items.length === 0) {
            previewEl.style.display = 'none';
            previewEl.innerHTML = '';
            return;
        }

        previewEl.innerHTML = '';
        items.forEach((it) => {
            const chip = document.createElement('span');
            chip.className = 'disciplina-preview-chip';
            chip.textContent = it;
            previewEl.appendChild(chip);
        });
        previewEl.style.display = '';
    };

    const pick = (name) => {
        const caret = textarea.selectionStart ?? textarea.value.length;
        const { prefix } = getActiveToken(textarea.value, caret);

        const existing = splitItems(prefix).map(titleCasePtBr);
        const next = [...existing, name].join(', ') + ', ';
        textarea.value = next;
        textarea.focus();
        textarea.setSelectionRange(textarea.value.length, textarea.value.length);
        hide();
        updatePreview();
    };

    let lastMatches = [];
    const updateSuggestions = () => {
        const caret = textarea.selectionStart ?? textarea.value.length;
        const { token, prefix } = getActiveToken(textarea.value, caret);
        const used = new Set(splitItems(prefix).map(s => normalizeText(titleCasePtBr(s))));

        const q = normalizeText(token);
        if (!q || q.length < 2) {
            hide();
            return;
        }

        const matches = normalizedAll
            .filter(x => x.n.includes(q) && !used.has(x.n))
            .slice(0, 8)
            .map(x => x.name);

        lastMatches = matches;
        renderSuggestions(listEl, matches, pick);
        panelEl.style.display = matches.length ? '' : 'none';
    };

    textarea.addEventListener('input', () => {
        // Normalização leve enquanto digita: sem espaço duplo e vírgula padronizada.
        // A "gramática" completa (Title Case) fica para o blur, para manter liberdade ao digitar.
        const caret = textarea.selectionStart ?? textarea.value.length;
        const raw = textarea.value;

        // Remover espaços duplos e normalizar vírgulas
        const normalized = raw
            .replace(/\s{2,}/g, ' ')
            .replace(/\s*,\s*/g, ', ');

        textarea.value = normalized;
        textarea.setSelectionRange(Math.min(caret, textarea.value.length), Math.min(caret, textarea.value.length));

        updatePreview();
        updateSuggestions();
    });

    textarea.addEventListener('blur', () => {
        // Formata no "final"
        applyFormatting();
        updatePreview();
        // pequena espera para permitir clique na sugestão
        setTimeout(hide, 120);
    });

    textarea.addEventListener('keydown', (ev) => {
        if (ev.key === 'Escape') {
            hide();
            return;
        }
    });

    document.addEventListener('click', (ev) => {
        if (!container.contains(ev.target)) hide();
    });

    if (btnInsertAll) {
        btnInsertAll.addEventListener('click', (ev) => {
            ev.preventDefault();
            if (!lastMatches || lastMatches.length === 0) return;

            const existing = splitItems(textarea.value).map(titleCasePtBr);
            const existingNorm = new Set(existing.map(e => normalizeText(e)));

            const toAdd = lastMatches.filter(m => !existingNorm.has(normalizeText(m)));
            const next = [...existing, ...toAdd].join(', ');
            textarea.value = next;
            textarea.focus();
            textarea.setSelectionRange(textarea.value.length, textarea.value.length);
            hide();
            updatePreview();
        });
    }

    if (btnOpenAll) {
        btnOpenAll.addEventListener('click', async (ev) => {
            ev.preventDefault();
            const existing = splitItems(textarea.value).map(titleCasePtBr);
            const picked = await openSuggestionPickerModal({
                title: 'Sugestões de Disciplinas',
                items: all,
                selectedItems: existing,
                maxItemsToShow: 80,
                maxSelected: 30
            });
            if (!picked) return;
            textarea.value = picked.map(titleCasePtBr).join(', ');
            textarea.focus();
            textarea.setSelectionRange(textarea.value.length, textarea.value.length);
            updatePreview();
        });
    }

    updatePreview();
}

