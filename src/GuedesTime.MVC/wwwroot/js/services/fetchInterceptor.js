import { loadingService } from './loadingService.js';
const originalFetch = window.fetch;
window.fetch = async function (...args) {
    loadingService.show();

    try {
        const response = await originalFetch(...args);
        return response;

    } catch (error) {
        console.error("Fetch Interceptor Error:", error);
        throw error;

    } finally {
        loadingService.hide();
    }
};