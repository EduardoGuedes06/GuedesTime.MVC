(function () {
    const validTypes = ['success', 'info', 'warning', 'error'];

    function displayMessage(msg, type = 'info') {
        const toastType = validTypes.includes(type) ? type : 'info';

        toastr.options = {
            closeButton: true,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: "5000"
        };

        toastr[toastType](msg);
    }
    window.showToast = displayMessage;
    $(function () {
        validTypes.forEach(type => {
            const val = $('#' + type).val();
            if (val) displayMessage(val, type);
        });
    });
})();
