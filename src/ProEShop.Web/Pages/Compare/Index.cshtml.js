function showAddProduct(e) {
    getHtmlWithAJAX('?handler=ShowAddProduct', null, 'showAddProductInModal', e);
}


function showAddProductInModal(result, clickedButton) {
    appendHtmlScrollableModalPlaceToBody();
    var currentModal = $('#html-scrollable-modal-place');
    currentModal.find('.modal-body').html(result);
    currentModal.modal('show');
    $('#html-scrollable-modal-place .modal-header h5').html('انتخاب کالا برای مقایسه');
    convertEnglishNumbersToPersianNumber();
}

$(document).on('click', '#add-product-modal-in-compare-page a', function (e) {
    e.preventDefault();

    var selectedProductCode = $(this).attr('product-code');

    var productCode1 = $('.product-item-in-compare-page').eq(0).attr('product-code');
    var productCode2 = $('.product-item-in-compare-page').eq(1).attr('product-code');
    var productCode3 = $('.product-item-in-compare-page').eq(2).attr('product-code');
    var productCode4 = selectedProductCode;

    var dataToSend = {
        productCode1: productCode1,
        productCode2: productCode2,
        productCode3: productCode3,
        productCode4: productCode4
    }

    closeScrollableHtmlModal();
    getHtmlWithAJAX('?handler=GetProductsForCompare', dataToSend, 'showProductsInCompareFunction', e);
});

function showProductsInCompareFunction(result) {
    $('#compare-page').html(result);
    convertEnglishNumbersToPersianNumber();
}