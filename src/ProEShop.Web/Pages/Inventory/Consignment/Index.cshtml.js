$(function () {
    fillDataTable();
    initializingAutocomplete();

    const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('delivery-date-icon-inventory-consignment'), {
        targetTextSelector: '#Consignments_SearchConsignments_DeliveryDate',
        persianNumber: true,
        selectedDate: new Date(),
        selectedDateToShow: new Date()
    });
});

function getProductDetails(e) {
    var productId = $(e).attr('product-id');
    getHtmlWithAJAX('?handler=GetProductDetails', { productId: productId }, 'showProductDetailsInModal', e);
}

function showProductDetailsInModal(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currnetModal = $('#html-modal-place');
    currnetModal.find('.modal-body').html(result);
    currnetModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
    initializeTinyMCE();
    $.validator.unobtrusive.parse($('#html-modal-place form'));
    activatingDeleteButtons(true);
}

// بعد از تایید و یا رد کردن محصول گرید را رفرش میکنیم
function productStatusInManagingProducts(message) {
    showToastr('success', message);
    $('#html-modal-place').modal('hide');
    fillDataTable();
}

function getConsignmentDetails(e) {
    var consignmentId = $(e).attr('consignment-id');
    getHtmlWithAJAX('?handler=GetConsignmentDetails', { consignmentId: consignmentId }, 'showCosignmentDetailsInModal', e);
}


function showCosignmentDetailsInModal(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currnetModal = $('#html-modal-place');
    currnetModal.find('.modal-body').html(result);
    currnetModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
}

function confirmationConsignment(message) {
    showToastr('success', message);
    fillDataTable();
}