﻿$(function () {
    fillDataTable();
    initializingAutocomplete();
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
}

// بعد از تایید و یا رد کردن محصول گرید را رفرش میکنیم
function productStatusInManagingProducts(message) {
    showToastr('success', message);
    $('#html-modal-place').modal('hide');
    fillDataTable();
}

function getProductVariants(e) {
    var productId = $(e).attr('product-id');
    getHtmlWithAJAX('?handler=ShowProductVariants', { productId: productId }, 'showProductVariantsInModal', e);
}

function showProductVariantsInModal(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currnetModal = $('#html-modal-place');
    currnetModal.find('.modal-body').html(result);
    currnetModal.modal('show');
    $('#html-modal-place .modal-header h5').html(
        'تنوع های من برای محصول: ' +
        $(clickedButton).parents('tr').find('td:eq(1)').html()
    );
}