fillDataTable();
appendHtmlModalPlaceToBody();

function getSellerDetails(e) {
    var sellerId = $(e).attr('seller-id');
    getHtmlWithAJAX('?handler=GetSellerDetails', { sellerId: sellerId }, 'showSellerDetailsInModal', e);
}

function showSellerDetailsInModal(result, clickedButton) {
    var currnetModal = $('#html-modal-place');
    currnetModal.find('.modal-body').html(result);
    currnetModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
}