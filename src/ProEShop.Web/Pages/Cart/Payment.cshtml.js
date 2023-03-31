$(function () {
    var hasValidationError = !isNullOrWhitespace($('form div:first li:first').text());
    if (hasValidationError) {
        showSweetAlert2('خطای اعتبار سنجی، لطفا خطا هایی که در بخش بالای درگاه نوشته شده اند را بررسی نمایید');
    }
    // موقعی که دکمه "اِن مرسوله" هاور شد
    // دراپ دان مِنو رو نشون بده
    // موقعی که بلر شد مخفیش کن
    $('#shipping-dropdown').hover(function () {
        $(this).dropdown('show');
    },
        function () {
            $(this).dropdown('hide');
        });

    // همینکه روی دکمه افزودن کد تخفیف کلیک شد
    // باکس افزودن کد تخفیف رو نمایش میده
    $('#show-discount-box-el-in-payment').click(function () {
        $(this).remove();
        $('#discount-code-box-in-payment').removeClass('d-none');
        $('#discount-code-box-in-payment input').focus();
    });

    // همینکه روی دکمه افزودن کارت هدیه کلیک شد
    // باکس افزودن کارت هدیه رو نمایش میده
    $('#show-gift-card-box-el-in-payment').click(function () {
        $(this).remove();
        $('#gift-card-code-box-in-payment').removeClass('d-none');
        $('#gift-card-code-box-in-payment input').focus();
    });

    var discountCodeValue = $('form input[name="CreateOrderAndPayModel.DiscountCode"]').val();
    // اگر فرم رفت سمت سرور و کد تخفیف مشکل داشته باشه، دوباره صفحه نمایش داده میشه و چون داخل اینپوت کد تخفیف مقدار هست
    // در نتیجه باید کلمه کد تخفیف رو دوباره داخل اینپوت کد تخفیف نمایش بدیم که کاربر بدونه کد تخفیف اشتباهی که وارد کرده بود چی بوده
    if (!isNullOrWhitespace(discountCodeValue)) {
        $('#discount-code-box-in-payment input').val(discountCodeValue);
        $('#show-discount-box-el-in-payment').click();
    }

    // اگر در داخل اینپوت کد تخفیف دکمه
    // enter
    // رو زد، به سمت سرور درخواست میفرستیم
    $('#discount-code-box-in-payment input').keypress(function (e) {
        if (e.which === 13) {
            $('#add-discount-code-button-in-payment').click();
        }
    });

    // اگر روی دکمه ثبت کد تخفیف کلیک شد، کد تخفیف رو به سمت سرور میفرسته که بررسی کنه همچین کدی وجود داره یا نه
    $('#add-discount-code-button-in-payment').click(function () {
        var code = $(this).parent().find('input').val();

        if (isNullOrWhitespace(code)) {
            showSweetAlert2('کد تخفیف نباید خالی باشد');
        } else {
            getDataWithAJAX('?handler=CheckForDiscount', { DiscountCode: code, SumPriceOfCart: finalPrice }, 'calculateDiscount');
        }
    });

    // اگر روی دکمه ثبت کارت هدیه کلیک شد، کارت هدیه رو به سمت سرور میفرسته که بررسی کنه همچین کارت هدیه ایی وجود داره یا نه
    $('#add-gift-card-code-button-in-payment').click(function () {
        var code = $(this).parent().find('input').val();

        if (isNullOrWhitespace(code)) {
            showSweetAlert2('کارت هدیه نباید خالی باشد');
        } else {
            getDataWithAJAX('?handler=CheckForGiftCard', { GiftCardCode: code, SumPriceOfCart: finalPrice }, 'calculateGiftCard');
        }
    });

    // اگر روی دکمه ضربدر کلیک شد باید کد تخفیف رو حذف کنیم
    $('#remove-discount-code-button-in-payment').click(function () {
        // مقدار اینپوت کد تخفیف رو حذف میکنیم
        // که موقعی که فرم به سمت سرور ارسال میشه، کد تخفیف مقدار نداشته باشه
        $('form').find('input[name="CreateOrderAndPayModel.DiscountCode"]').val('');

        // نمایش دکمه ثبت
        $(this).addClass('d-none');
        // مخفی کردن دکمه ثبت
        $(this).prev('button').removeClass('d-none');
        // اینپوت کد تخفیف
        var inputEl = $(this).parent().find('input');
        inputEl.val('');
        inputEl.focus();
        // مخفی کردن متن زیر اینپوت که میگه: کد تخفیف اعمال شد
        $('#discount-code-added-text-in-payment').addClass('d-none');
        // مخفی کردن بخش کد تخفیف در سمت چپ صفحه
        $('#show-discount-code-box-in-payment').addClass('d-none');
        // محاسبه مجدد قیمت ها
        calculatePrices(0);
    });
});

// بعد از اینکه کد تخیف رو به سمت سرور فرستادیم، نتیجه به این فانکشن برگشت داده میشه
function calculateDiscount(message, data) {
    calculateDiscountAndGiftCard(data, true);
}

// بعد از اینکه کارت هدیه رو به سمت سرور فرستادیم، نتیجه به این فانکشن برگشت داده میشه
function calculateGiftCard(message, data) {
    calculateDiscountAndGiftCard(data, false);
}

function calculateDiscountAndGiftCard(data, isDiscount) {
    var input = isDiscount ? 'Discount' : 'GiftCard';
    var elName = isDiscount ? 'discount' : 'gift-card';

    if (data.result) {
        // تغییر مقدار اینپوت که موقعی که دکمه پرداخت رو میزنیم
        // کد تخفیف یا کارت هدیه به سمت سرور ارسال بشه
        var discountCode = $('#discount-code-box-in-payment').find('input').val();
        $('form').find('input[name="CreateOrderAndPayModel.' + input + 'Code"]').val(discountCode);

        // اینپوت رو از حالت فوکس خارج میکنیم
        $('#' + elName + '-code-box-in-payment input').blur();
        // نمایش دکمه ضربدر
        $('#remove-' + elName + '-code-button-in-payment').removeClass('d-none');
        // مخفی کردن دکمه ثبت
        $('#add-' + elName + '-code-button-in-payment').addClass('d-none');
        // نمایش متن  "کد تخفیف یا کارت اعمال شد" در پایین اینپوت
        $('#' + elName + '-code-added-text-in-payment').removeClass('d-none');
        // نمایش دادن بخش تخفیف در سمت چپ صفحه، چه برای کارت هدیه چه برای کد تخفیف نمایش داده میشه
        // این باکس اصلیه تخفیفه چه برای کد تخفیف چه برای کارت هدیه
        $('#show-' + elName + '-code-box-in-payment').removeClass('d-none');
        // نمایش مقدار کارت هدیه و کد تخفیف به صورت فارسی
        $('#' + elName + '-code-price-box-in-payment').html(data.discountPrice.toString().addCommaToDigits().toPersinaDigit());
    } else {
        // مقدار اینپوت رو حذف میکنیم
        // که موقعی که فرم به سمت سرور ارسال میشه، مقدار نداشته باشه
        $('form').find('input[name="CreateOrderAndPayModel.' + input + 'Code"]').val('');

        // اینپوت رو دوباره به حالت فوکس در میاریم
        $('#' + elName + '-code-box-in-payment input').focus();
        // مخفی کردن دکمه ضربدر
        $('#remove-' + elName + '-code-button-in-payment').addClass('d-none');
        // نمایش دکمه ثبت
        $('#add-' + elName + '-code-button-in-payment').removeClass('d-none');
        // مخفی کردن متن "کد تخفیف یا کارت هدیه اعمال شد" در پایین اینپوت 
        $('#' + elName + '-code-added-text-in-payment').addClass('d-none');
        // مخفی کردن مقدار تخفیف در بخش چپ صفحه
        $('#show-' + elName + '-code-box-in-payment').addClass('d-none');
        showSweetAlert2(data.message);
    }

    // محاسبه مجدد قیمت ها بعد از اضافه شدن مقدار کد تخفیف
    calculatePrices(data.discountPrice);
}

// به محض لود صفحه این مقادیر رو میگیریم که در هنگام افزودن کد تخفیف بتونیم
// قیمت های جدید رو محاسبه کنیم
var totalPrice = parseInt($('#total-price-box-in-payment').html().trim().replace(/,/g, '').toEnglishDigit());
var discountPrice = parseInt($('#discount-price-box-in-payment span:last').html().trim().replace(/,/g, '').toEnglishDigit());
var finalPrice = totalPrice - discountPrice;

// محاسبه مجدد قیمت ها بعد از اضافه شدن مقدار کد تخفیف
function calculatePrices(discountCodePrice) {
    // قیمت نهایی منهای میزان کد تخفیف که کاربر باید این مقدار رو پرداخت کنه
    var finalPriceWithDiscountCodePrice = finalPrice - discountCodePrice;

    // قابل پرداخت 10 هزاره، کد تخفیف 20 هزار، در نتیجه قابل پرداخت منفی میشه
    if (finalPriceWithDiscountCodePrice <= 0) {
        finalPriceWithDiscountCodePrice = 0;
    }

    // در قسمت قابل پرداخت مقدار متغیر بالایی رو نمایش میدیم
    $('#final-price-box-in-payment')
        .html(finalPriceWithDiscountCodePrice.toString().addCommaToDigits().toPersinaDigit());

    // اگر بخش "سود شما از خرید" به دلیل نبود تخفیف مخفی بود، اون رو نمایش میدیم
    // که میزان کد تخفیف رو در اونجا نمایش بدیم
    $('#discount-price-box-in-payment').removeClass('d-none');
    // محاسبه مجدد میزان تخفیف کاربر
    // تخفیف خود کالا ها به علاوه مقدار کد تخفیف

    // مقدار تخفیف کالا به علاوه مقدار کد تخفیف
    var discountPriceWithDiscountCodePrice = discountPrice + discountCodePrice;

    // نباید در بخش سود خرید، مبلغی بیشتر از توتال پرایس نمایش داده بشه
    if (discountPriceWithDiscountCodePrice > totalPrice) {
        discountPriceWithDiscountCodePrice = totalPrice;
    }

    $('#discount-price-box-in-payment span:last')
        .html(discountPriceWithDiscountCodePrice.toString().addCommaToDigits().toPersinaDigit());

    // محاسبه درصد تخفیف
    // کل توضیحات در ریزر پیج
    // Payment
    var totalPriceDivideBy100 = Math.ceil(totalPrice / 100);

    // چرا به پایین گرد  میکنیم ؟
    // چون تا زمانی که حتی یک تومان هم پرداخت کنیم، تخفیف، صد در صد نیست
    // برای مثال قیمت سبد خرید ده هزاره
    // حالا یک کد تخفیف
    // 9999
    // تومانی داریم، اگر محاسبه کنیم یک عدد بین 99 و 100 به دست میاد
    // و اگه به بالا گرد بشه میشه 100، درصورتیکه ما حداقل یک تومان پرداخت میکنیم پس نمیشه گفت که ما
    // تخفیف 100 درصدی داریم
    var percentageOfTotalPriceOfCartThatUserMustPay = Math.floor(finalPriceWithDiscountCodePrice / totalPriceDivideBy100);

    var discountPercentage = 100 - percentageOfTotalPriceOfCartThatUserMustPay;

    // اگر محصولات تخفیف نداشته باشن بخش سود شما از خرید نمایش داده نمیشه ومخفیه
    // و بعد اگه دکمه ضربدر رو بزنیم که کد تخفیف حذف بشه
    // باید دوباره بخش سود شما از خرید رو مخفی کنیم
    // چون از اول وجود نداشته و الان هم کد تخفیف نداریم که بخوایم بخش سود شما از خرید رو نمایش بدیم
    if (discountPercentage === 0) {
        $('#discount-price-box-in-payment').addClass('d-none');
    }

    // درصد تخفیف
    $('#discount-price-box-in-payment span:first')
        .html(discountPercentage.toString().toPersinaDigit());
}

// همینکه روی دکمه پرداخت کلیک بشه، دکمه رو غیر فعال کنیم که مجددا روی دکمه کلیک نشه
$('form').submit(function () {
    $('#create-order-and-pay-button').attr('disabled', 'disabled');
});