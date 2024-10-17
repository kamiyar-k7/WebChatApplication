function OpenBootStrapModal(idmodal) {
    const mymodal = new bootstrap.Modal('#' + idmodal);
    mymodal.show();
}

function CloseBootStrapModal(modalId) {
    var modalElement = document.getElementById(modalId);
    if (modalElement) {
        var modal = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);
        modal.hide();
    }
}