$(function () {
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
    var currentModal = $('#html-modal-place');
    currentModal.find('.modal-body').html(result);
    convertEnglishNumbersToPersianNumber();
    currentModal.modal('show');
    $('#html-modal-place .modal-header h5').html(
        'تنوع های من برای محصول: ' +
        $(clickedButton).parents('tr').find('td:eq(1)').html()
    );
}

function editProductVariant(e) {
    var productVariantId = $(e).attr('product-variant-id');
    $('#html-modal-place').modal('hide');
    getHtmlWithAJAX('?handler=EditProductVariant', { productVariantId: productVariantId }, 'editProductVariantInModal', e);
}

function editProductVariantInModal(result, clickedButton) {
    appendSecondHtmlModalPlaceToBody();
    var currentModal = $('#second-html-modal-place');
    currentModal.find('.modal-body').html(result);
    $.validator.unobtrusive.parse(currentModal.find('form'));
    currentModal.modal('show');
    $('#second-html-modal-place .modal-header h5').html(
        'ویرایش تنوع محصول'
    );
}

function editProductVariantFunction(message) {
    showToastr('success', message);
}