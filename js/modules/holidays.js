import { appData } from '../state.js';
import { showToast } from '../ui.js';

const holidaysList = document.getElementById('holidays-list');
const addHolidayBtn = document.getElementById('add-holiday-btn');

export function renderHolidays() {
    if (!holidaysList) return;
    holidaysList.innerHTML = '';

    if (appData.holidays.length === 0) {
        holidaysList.innerHTML = '<p class="placeholder-text">Nenhum feriado cadastrado.</p>';
        return;
    }

    appData.holidays.forEach(holiday => {
        const item = document.createElement('div');
        item.className = 'list-item';
        const holidayDate = new Date(holiday.date);
        const formattedDate = new Date(holidayDate.getTime() + holidayDate.getTimezoneOffset() * 60000).toLocaleDateString('pt-BR');

        item.innerHTML = `
            <div>
                <p class="list-item-title">${formattedDate}</p>
                <p class="list-item-subtitle">${holiday.description}</p>
            </div>
            <div class="actions">
                <button class="btn primary-btn btn-sm" onclick="showToast('Ação desabilitada no protótipo.', 'info')"><i class="fas fa-edit"></i></button>
                <button class="btn danger-btn btn-sm" onclick="showToast('Ação desabilitada no protótipo.', 'info')"><i class="fas fa-trash-alt"></i></button>
            </div>
        `;
        holidaysList.appendChild(item);
    });

    if (!window.showToast) {
        window.showToast = showToast;
    }
}

export function initHolidays() {
    if (document.getElementById('holidays')) {
        addHolidayBtn.addEventListener('click', () => {
            showToast('Modal não implementado neste protótipo.', 'info');
        });
        renderHolidays();
    }
}