import { appData } from '../state.js';
import { showToast } from '../ui.js';
export function renderInstituicao() {

}

export function initInstituicao() {
    if (document.getElementById('instituicao')) {
        addHolidayBtn.addEventListener('click', () => {
            showToast('Modal não implementado neste protótipo.', 'info');
        });
        renderInstituicao();
    }
}