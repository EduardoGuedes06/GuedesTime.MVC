import { appData } from '../state.js';
import { showToast } from '../ui.js';

const timeSlotsBody = document.getElementById('time-slots-body');
const addTimeSlotBtn = document.getElementById('add-time-slot-btn');

export function renderTimeSlots() {
    if (!timeSlotsBody) return;
    timeSlotsBody.innerHTML = '';

    if (appData.timeSlots.length === 0) {
        const row = document.createElement('tr');
        row.innerHTML = '<td colspan="4" class="placeholder-text">Nenhuma faixa de horário cadastrada.</td>';
        timeSlotsBody.appendChild(row);
        return;
    }

    appData.timeSlots.forEach(slot => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${slot.day}</td>
            <td>${slot.startTime}</td>
            <td>${slot.endTime}</td>
            <td class="actions-cell">
                <button onclick="showToast('Ação desabilitada no protótipo.', 'info')" class="btn-icon"><i class="fas fa-edit"></i></button>
                <button onclick="showToast('Ação desabilitada no protótipo.', 'info')" class="btn-icon delete"><i class="fas fa-trash-alt"></i></button>
            </td>
        `;
        timeSlotsBody.appendChild(row);
    });

    if (!window.showToast) {
        window.showToast = showToast;
    }
}

export function initTimeSlots() {
    if (document.getElementById('time-slots')) {
        addTimeSlotBtn.addEventListener('click', () => {
            showToast('Modal não implementado neste protótipo.', 'info');
        });
        renderTimeSlots();
    }
}