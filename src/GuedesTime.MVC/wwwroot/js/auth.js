import { showToast } from './ui.js';

document.addEventListener('DOMContentLoaded', () => {
    const successMessage = document.getElementById('tempdata-success-message')?.value;
    const errorMessage = document.getElementById('tempdata-error-message')?.value;
    const infoMessage = document.getElementById('tempdata-info-message')?.value;
    const warningMessage = document.getElementById('tempdata-warning-message')?.value;
    if (successMessage) showToast(successMessage, 'success');
    if (errorMessage) showToast(errorMessage, 'error');
    if (infoMessage) showToast(infoMessage, 'info');
    if (warningMessage) showToast(warningMessage, 'warning');
});