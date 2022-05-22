$(function () {
    fillDataTable();
});

function getSellerDetails(e) {
    var sellerId = $(e).attr('seller-id');
    getHtmlWithAJAX('?handler=GetSellerDetails', { sellerId: sellerId }, 'showSellerDetailsInModal', e);
}

function showSellerDetailsInModal(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currnetModal = $('#html-modal-place');
    currnetModal.find('.modal-body').html(result);
    currnetModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
    initializeTinyMCE();
    $.validator.unobtrusive.parse($('#html-modal-place form'));
    activatingDeleteButtons(true);
}

// بعد از تایید و یا رد کردن فروشنده گرید را رفرش میکنیم
function sellerDocumentInManagingSellers(message) {
    showToastr('success', message);
    $('#html-modal-place').modal('hide');
    fillDataTable();
}