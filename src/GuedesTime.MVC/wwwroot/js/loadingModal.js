const modal = document.getElementById('global-loading-modal');
let requestCount = 0;
let timeoutId = null;

function show() {
    requestCount++;
    if (requestCount === 1) {
        timeoutId = setTimeout(() => {
            if (modal) {
                document.body.setAttribute('aria-busy', 'true');
                modal.hidden = false;
            }
        }, 300);
    }
}
function hide() {
    requestCount = Math.max(0, requestCount - 1);
    if (requestCount === 0) {
        clearTimeout(timeoutId);
        if (modal) {
            document.body.removeAttribute('aria-busy');
            modal.hidden = true;
        }
    }
}

export const loadingModal = {
    show,
    hide
};