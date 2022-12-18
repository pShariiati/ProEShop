$(function () {
    // حذف محصولات
    $(document).on('click', '.remove-button-in-compare-page', function () {
        $(this).parents('.product-item-in-compare-page').remove();

        var productCode1 = $('.product-item-in-compare-page').eq(0).attr('product-code');
        var productCode2 = $('.product-item-in-compare-page').eq(1).attr('product-code');
        var productCode3 = $('.product-item-in-compare-page').eq(2).attr('product-code');

        var dataToSend = {
            productCode1: productCode1,
            productCode2: productCode2,
            productCode3: productCode3
        }

        // اگر محصولات صفحه تغییر پیدا کردند (چه حذف چه اضافه)، باید مصحصولات داخل مودال
        // از نو از پایگاه داده دریافت شوند
        isModalOpened = false;
        getHtmlWithAJAX('/compare/index?handler=GetProductsForCompare', dataToSend, 'showProductsInCompareFunction', null);
    });

    // چطور بعد از اینکه کاربر نیم ثانیه تایپ نکرد، درخواست رو
    // به سمت سرور ارسال کنیم
    // https://stackoverflow.com/a/1909490/16180500
    // https://t.me/PieCodeTel/130
    var globalTimeout = null;
    $(document).on('keyup', '#search-input-in-compare-page', function () {
        if (globalTimeout != null) {
            clearTimeout(globalTimeout);
        }
        globalTimeout = setTimeout(function () {
            // اگر مقداری که در داخل متغیر هست با اونیکه داخل اینپوت هست یکی باشه
            // یعنی کاربر متن جستجو رو تغییر نداده پس نیازی نیست که درخواستی هم به
            // سمت سرور ارسال بشه، ما باید فقط در مواقعی به سمت سرور درخواست بفرستیم که
            // متن جدیدی وارد شده باشه، برای مثال بار اول سرچ میکنه تست باز هم همون تست رو وارد میکنه
            // دیگه نیازی نیست برای بار دوم درخواستی به سمت سرور ارسال بشه
            // این ایونت حتی برای فشار دادن دکمه های تغییر زبان هم اجرا میشه، این ایونت جلوی ارسال شدن
            // به سمت سرور در صورت فشار دادن دکمه های تغییر زبان رو هم میگیره
            if (searchValue !== $('#search-input-in-compare-page').val().trim()) {
                searchValue = $('#search-input-in-compare-page').val().trim();
                // موقعی که به سمت سرور درخواست رو میفرستیم اینپوت سرچ
                // رو غیر فعال میکنیم
                $('#search-input-in-compare-page').attr('disabled', 'disabled');

                // هدر بخش مودال رو که شامل تعداد محصولات هست مخفی میکنیم
                $('#add-product-header-in-compare-page').addClass('d-none');

                // موقعی که جستجو انجام میشه، قطعا تمامی محصولات داخل مودال هم از نو مقدار دهی میشن
                // پس اونارو کامل حذف میکنیم تا به همراه مخفی کردن هدر بالای مودال، فقط
                // لودینگ نمایش داده بشه
                $('#add-product-modal-in-compare-page').html('');
                isProcessing = true;
                searchViaInput = true;
                $('#compare-partial-loading').removeClass('d-none');

                // اگر هیچ چیزی رو سرچ نکرده بود، کل محصولات رو نمایش بده
                if (searchValue === '') {
                    getHtmlWithAJAX('?handler=ShowAddProduct', { productCodesToHide: getProductCodesToHide() }, 'showAddProductInModal', null, false);
                } else {
                    var dataToSend = {
                        productCodesToHide: getProductCodesToHide(),
                        searchValue: searchValue
                    }
                    pageNumber = 1;
                    getHtmlWithAJAX('?handler=ShowAddProduct', dataToSend, 'showAddProductInModal', null, false);
                }
            }
        }, 500);
    });
});

// اگر مودال افزودن محصولات باز شده بود دیگر نیازی نیست که
// از نو اطلاعات داخل مودال رو از سرور بگیریم
var isModalOpened = false;

// مقداری که کاربر جستجو کرده است
var searchValue = '';

// اگر سرچ از طریق اینپوت صورت گرفته است
// باید اینپوت رو بعد از اینکه ریسپانس اومد، در حالت
// فوکس قرار بدیم
var searchViaInput = false;

// برای صفحه بندیِ مودال افزودن محصول
var pageNumber = 1;

// آیا آخرین صفحه لود شده است ؟
var isLastPage = false;

// آیا کاربر به سمت سرور یک درخواست ارسال کرده است ؟
// اگر ترو باشد، نباید اجازه دهیم در هنگامی که درخواستی به سمت
// سرور ارسال کرده، یکبار دیگر درخواست مجددی ارسال کند
var isProcessing = false;

function showAddProduct(e) {
    if (isModalOpened === false) {
        getHtmlWithAJAX('?handler=ShowAddProduct', { productCodesToHide: getProductCodesToHide() }, 'showAddProductInModal', e);
    } else {
        var currentModal = $('#html-scrollable-modal-place');
        currentModal.modal('show');
    }
}

// برای مثال در صفحه مقایسه دو محصول اضافه شده اند
// حالا نباید در داخل مودال افزودن محصول این دو محصول را
// به کاربر نمایش دهیم چون از قبل اضافه شده اند
function getProductCodesToHide() {
    var productCode1 = $('.product-item-in-compare-page').eq(0).attr('product-code');
    var productCode2 = $('.product-item-in-compare-page').eq(1).attr('product-code');
    var productCode3 = $('.product-item-in-compare-page').eq(2).attr('product-code');

    var productCodesToHide = [];

    productCodesToHide.push(productCode1);
    productCodesToHide.push(productCode2);
    productCodesToHide.push(productCode3);

    return productCodesToHide;
}

function showAddProductInModal(result, clickedButton) {
    // موقعی که اطلاعات از سمت سرور برگشت داده شدند، این مورد رو فالس میکنیم
    // چون درخواستی سمت سرور وجود ندارد
    isProcessing = false;
    isModalOpened = true;

    appendHtmlScrollableModalPlaceToBody('modal-lg');

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
                var dataToSend = {
                    pageNumber: ++pageNumber,
                    productCodesToHide: getProductCodesToHide(),
                    searchValue: searchValue
                }
                getHtmlWithAJAX('?handler=ShowAddProduct', dataToSend, 'showAddProductInModal', e, false);
            }
        }
    });

    var currentModal = $('#html-scrollable-modal-place');

    // اگر صفحه یک درخواست شده بود، باید کل محتوای داخل مودال رو تغییر بدیم
    // اگر هم صفحه یک به بالا درخواست شده بود باید فقط بخش محصولات رو تغییر بدیم
    if (result.data.pageNumber === 1) {
        currentModal.find('.modal-body').html(result.data.productsBody);
    } else {
        currentModal.find('.modal-body #add-product-modal-in-compare-page').append(result.data.productsBody);
        $('#product-count-in-compare-page').html(result.data.productsCount);
    }

    isLastPage = result.data.isLastPage;

    // اگر صفحه آخر لود شد، باید گیف لودینگ رو مخفی کنیم
    if (result.data.isLastPage) {
        $('#compare-partial-loading').addClass('d-none');
    } else {
        $('#compare-partial-loading').removeClass('d-none');
    }

    // چون محتویات داخل مودال به طور کل از نو جایگذاری میشه
    // پس مقدار داخل اینپوت سرچ هم از بین میره
    // به خاطر همین از نو، مقداری که کاربر قبل زدن درخواست به سمت
    // سرور، سرچ کرده بود رو دوباره در داخل اینپوت نمایش میدیم
    $('#search-input-in-compare-page').val(searchValue);

    if (searchViaInput) {
        searchViaInput = false;
        $('#search-input-in-compare-page').focus();
    }

    currentModal.modal('show');
    $('#html-scrollable-modal-place .modal-header h5').html('انتخاب کالا برای مقایسه');
    convertEnglishNumbersToPersianNumber();
}

// کلیک روی محصولات داخل مودال
$(document).on('click', '#add-product-modal-in-compare-page a', function (e) {
    e.preventDefault();

    // اگر سمت سرور بودیم نباید اجازه بدیم محصولی رو انتخاب کنه
    // چون مودال بسته میشه و همینکه صفحه بعدی محصولات لود بشه، مودال بسته شده دوباره نمایش داده میشه
    if (isProcessing) {
        return;
    }

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

    // اگر محصولات صفحه تغییر پیدا کردند (چه حذف چه اضافه)، باید مصحصولات داخل مودال
    // از نو از پایگاه داده دریافت شوند
    isModalOpened = false;
    closeScrollableHtmlModal();
    getHtmlWithAJAX('/compare/index?handler=GetProductsForCompare', dataToSend, 'showProductsInCompareFunction', e);
});

function showProductsInCompareFunction(result) {
    $('#compare-page').html(result);

    // اگر تعداد محصولات این صفحه یک بود، باید دکمه حذف کردن رو مخفی بکنیم
    if ($('.product-item-in-compare-page').length === 1) {
        $('.product-item-in-compare-page:first').find('div:first').addClass('invisible');
    }
    convertEnglishNumbersToPersianNumber();
}