// برای صفحه بندیِ مودال افزودن محصول
var pageNumber = 1;

// آیا آخرین صفحه لود شده است ؟
var isLastPage = false;

// آیا کاربر به سمت سرور یک درخواست ارسال کرده است ؟
// اگر ترو باشد، نباید اجازه دهیم در هنگامی که درخواستی به سمت
// سرور ارسال کرده، یکبار دیگر درخواست مجددی ارسال کند
var isProcessing = false;

function showAddProduct(e) {
    getHtmlWithAJAX('?handler=ShowAddProduct', null, 'showAddProductInModal', e);
}

function showAddProductInModal(result, clickedButton) {

    // موقعی که اطلاعات از سمت سرور برگشت داده شدند، این مورد رو فالس میکنیم
    // چون درخواستی سمت سرور وجود ندارد
    isProcessing = false;

    appendHtmlScrollableModalPlaceToBody();
    
    $('#html-scrollable-modal-place .modal-body').off('scroll').scroll(function (e) {

        // کل عرض المنت
        // برای مثال هزار پیکسل
        var elementHeight = this.scrollHeight;

        // کل عرض المنت که قابل مشاهده است، برای مثال کل
        // عرض هزار پیکسل است، اما فقط 300 پیکسل قابل مشاهده است و برای مشاهده مابقی عرض المنت
        // باید به پایین اسکرول کنیم
        var elementVisibleHeight = this.offsetHeight;

        // چند پیکسل از سمت بالا به پایین اسکرول کرده ایم
        var scrollFromTop = this.scrollTop;

        if (scrollFromTop + 20 >= elementHeight - elementVisibleHeight) {

            // اگر صفحه آخر لود شده بود نباید به سمت سرور درخواستی ارسال کنیم
            // اگر درخواستی به سمت سرور زده شده است
            // سرور مشغول درخواست ما میباشد، پس نباید در هنگامی که سرور مشغول  به یک درخواست است
            // یک درخواست دیگر صادر کنیم
            if (isLastPage === false && isProcessing === false) {
                isProcessing = true;
                getHtmlWithAJAX('?handler=ShowAddProduct', { pageNumber: ++pageNumber }, 'showAddProductInModal', e, false);
            }
        }
    });

    var currentModal = $('#html-scrollable-modal-place');

    isLastPage = result.data.isLastPage;

    // اگر صفحه آخر لود شد، باید گیف لودینگ رو مخفی کنیم
    if (result.data.isLastPage) {
        $('#compare-partial-loading').addClass('d-none');
    } else {
        $('#compare-partial-loading').removeClass('d-none');
    }

    // اگر صفحه یک درخواست شده بود، باید کل محتوای داخل مودال رو تغییر بدیم
    // اگر هم صفحه یک به بالا درخواست شده بود باید فقط بخش محصولات رو تغییر بدیم
    if (result.data.pageNumber === 1) {
        currentModal.find('.modal-body').html(result.data.productsBody);
    } else {
        currentModal.find('.modal-body #add-product-modal-in-compare-page').append(result.data.productsBody);
    }

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