$(function () {

    $(document).on('change', '#OffPercentage', function() {
        var price = $('#Price').val();
        var offPercentage = $('#OffPercentage').val();
        var discountPrice = price / 100 * offPercentage;
        var priceWithDiscount = Math.ceil(price - discountPrice);
        $('#OffPrice').val(priceWithDiscount);
    });

    $(document).on('change', '#OffPrice', function() {
        var offPrice = $('#OffPrice').val();
        var price = $('#Price').val();
        var offPercentage = $('#OffPercentage').val();
        var discountPrice = price / 100 * offPercentage;
        var priceWithDiscount = Math.ceil(price - discountPrice);
        var discountPriceSubtract1Percentage = price / 100 * (offPercentage - 1);
        var priceWithDiscountSubtract1Percentage = Math.floor(price - discountPriceSubtract1Percentage);

        // برای مثال قیمت کالا هزارتومان است
        // درصد تخفیف 7 درصد
        // یعنی میزان تخفیف 70 تومان است و مبلغ نهایی 930 تومان است
        // اگر قیمتی که در اینپوت تخفیف وارد میشود کمتر از 930 تومان باشد وارد
        // شاخه 8 درصد تخفیف میشود، پس باید به کاربر خطا نمایش دهیم
        // ما مخواهیم میزان تخفیف بین 6 تا 7 درصد باشد
        // بزرگتر از 6 و کوچکتر و مساوی 7 درصد
        // یعنی
        // OffPrice >= 930 && OffPrice < 940
        // شش درصد تخفیف روی هزارتومان میشود 940 تومان
        // اگر قرار است که مبلغ 940 تومان باشد پس باید شش درصد تخفیف وارد شود نه 7 درصد
        if (offPrice < priceWithDiscount || offPrice >= priceWithDiscountSubtract1Percentage) {
            showErrorMessage(`قیمت تخفیف باید بزرگتر مساوی ${priceWithDiscount.toString().addCommaToDigits().toPersinaDigit()} تومان و کوچکتر از ${priceWithDiscountSubtract1Percentage.toString().addCommaToDigits().toPersinaDigit()} تومان باشد`);
        }
    });

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

function addEditDiscount(e) {
    var productVariantId = $(e).attr('product-variant-id');
    $('#html-modal-place').modal('hide');
    getHtmlWithAJAX('?handler=AddEditDiscount', { productVariantId: productVariantId }, 'addEditDiscountInModal', e);
}

function addEditDiscountInModal(result, clickedButton) {
    appendSecondHtmlModalPlaceToBody();
    var currentModal = $('#second-html-modal-place');
    currentModal.find('.modal-body').html(result);
    activatingDateTimePicker('start-datetime-add-edit-discount', 'StartDateTime');
    activatingDateTimePicker('end-datetime-add-edit-discount', 'EndDateTime');
    $.validator.unobtrusive.parse(currentModal.find('form'));
    currentModal.modal('show');
    $('#second-html-modal-place .modal-header h5').html(
        'ایجاد / ویرایش تخفیف'
    );
}

function activatingDateTimePicker(spanId, inputId) {
    new mds.MdsPersianDateTimePicker(document.getElementById(spanId), {
        targetTextSelector: `#${inputId}`,
        persianNumber: true,
        enableTimePicker: true,
        selectedDate: new Date($(`#${inputId}`).attr('date-en') || new Date()),
        selectedDateToShow: new Date($(`#${inputId}`).attr('date-en') || new Date())
    });
}

function addEditDiscountFunction(message) {
    showToastr('success', message);
}