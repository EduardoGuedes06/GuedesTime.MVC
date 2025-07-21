import { appData } from '../state.js';
import { showToast } from '../ui.js';

const classroomsList = document.getElementById('classrooms-list');
const addClassroomBtn = document.getElementById('add-classroom-btn');

export function renderClassrooms() {
    if (!classroomsList) return;
    classroomsList.innerHTML = '';

    if (appData.classrooms.length === 0) {
        classroomsList.innerHTML = '<p class="placeholder-text">Nenhuma sala de aula cadastrada.</p>';
        return;
    }

    appData.classrooms.forEach(classroom => {
        const card = document.createElement('div');
        card.className = 'entity-card';
        card.innerHTML = `
            <h3>${classroom.name}</h3>
            <p>Localização: ${classroom.location}</p>
            <p>Capacidade: ${classroom.capacity} alunos</p>
            <div class="actions">
                <button class="btn primary-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Editar</button>
                <button class="btn danger-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Excluir</button>
            </div>
        `;
        classroomsList.appendChild(card);
    });

    if (!window.showToast) {
        window.showToast = showToast;
    }
}

export function initClassrooms() {
    if (document.getElementById('classrooms')) {
        addClassroomBtn.addEventListener('click', () => {
            showToast('Modal não implementado neste protótipo.', 'info');
        });
        renderClassrooms();
    }
}