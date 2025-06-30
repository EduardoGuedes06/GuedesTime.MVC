import { appData } from '../state.js';
import { showToast } from '../ui.js';

const seriesList = document.getElementById('series-list');
const addSeriesBtn = document.getElementById('add-series-btn');
const seriesSearchInput = document.getElementById('series-search');

export function renderSeries(searchQuery = '') {
    if (!seriesList) return;

    const filteredSeries = appData.series.filter(_series =>
        _series.name.toLowerCase().includes(searchQuery.toLowerCase())
    );

    seriesList.innerHTML = '';
    if (filteredSeries.length === 0) {
        seriesList.innerHTML = '<p class="placeholder-text">Nenhuma série encontrada.</p>';
        return;
    }

    filteredSeries.forEach(_series => {
        const card = document.createElement('div');
        card.className = 'entity-card';
        card.innerHTML = `
            <h3>${_series.name}</h3>
            <p>Ordem: ${_series.order}</p>
            <div class="info-row">
                <span>Disciplinas Relacionadas:</span>
                <div class="badges-container">
                    ${_series.subjectIds.map(subId => {
            const subject = appData.subjects.find(s => s.id === subId);
            return subject ? `<span class="badge subject-badge">${subject.name}</span>` : '';
        }).join('')}
                </div>
            </div>
            <div class="actions">
                <button class="btn primary-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Editar</button>
                <button class="btn danger-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Excluir</button>
            </div>
        `;
        seriesList.appendChild(card);
    });

    if (!window.showToast) {
        window.showToast = showToast;
    }
}

export function initSeries() {
    if (document.getElementById('series')) {
        addSeriesBtn.addEventListener('click', () => {
            showToast('Modal não implementado neste protótipo.', 'info');
        });
        seriesSearchInput.addEventListener('input', (e) => renderSeries(e.target.value));
        renderSeries();
    }
}