import { appData } from '../state.js';
import { showToast } from '../ui.js';

// A variável 'newLessonPlanBtn' foi REMOVIDA daqui.
const lessonPlanList = document.getElementById('lesson-plan-versions-list');

export function renderLessonPlans() {
    if (!lessonPlanList) return;
    lessonPlanList.innerHTML = '';

    if (appData.lessonPlans.length === 0) {
        lessonPlanList.innerHTML = '<p class="placeholder-text">Nenhum plano de aula encontrado.</p>';
        return;
    }

    const sortedPlans = [...appData.lessonPlans].sort((a, b) => new Date(b.createdDate) - new Date(a.createdDate));

    sortedPlans.forEach(plan => {
        let statusBadgeClass = '';
        if (plan.status === 'Approved') {
            statusBadgeClass = 'status-approved';
        } else if (plan.status === 'Pending') {
            statusBadgeClass = 'status-pending';
        } else if (plan.status === 'Conflicted') {
            statusBadgeClass = 'status-conflicted';
        }

        const item = document.createElement('div');
        item.className = 'list-item';
        const createdDate = new Date(plan.createdDate).toLocaleDateString('pt-BR');

        item.innerHTML = `
            <div>
                <p class="list-item-title">Versão ${plan.version} - ${createdDate}</p>
                <span class="badge ${statusBadgeClass}">${plan.status}</span>
            </div>
            <div class="actions">
                <button class="btn secondary-btn btn-sm" onclick="showToast('Visualização não implementada.', 'info')">Visualizar</button>
            </div>
        `;
        lessonPlanList.appendChild(item);
    });

    if (!window.showToast) {
        window.showToast = showToast;
    }
}

export function initLessonPlans() {
    if (document.getElementById('lesson-plans')) {
        const newLessonPlanBtn = document.getElementById('new-lesson-plan-btn');
        if (newLessonPlanBtn) {
            newLessonPlanBtn.addEventListener('click', () => {
                showToast('Criação de novo plano não implementada.', 'info');
            });
        }
        
        renderLessonPlans();
    }
}