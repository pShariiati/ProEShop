$(function () {
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

    // test
    // remove this in production
    $('#show-discount-box-el-in-payment').click();

    // اگر در داخل اینپوت کد تخفیف دکمه
    // enter
    // رو زد، به سمت سرور درخواست میفرستیم
    $('#discount-code-box-in-payment input').keypress(function(e) {
        if (e.which === 13) {
            $('#add-discount-code-button-in-payment').click();
        }
    });

    // اگر روی دکمه ثبت کد تخفیف کلیک شد، کد تخفیف رو به سمت سرور میفرسته که بررسی کنه همچین کدی وجود داره یا نه
    $('#add-discount-code-button-in-payment').click(function() {
        var code = $(this).parent().find('input').val();
        getDataWithAJAX('?handler=CheckForDiscount', { code }, 'calculateDiscount');
    });
});

// بعد از اینکه کد تخیف رو به سمت سرور فرستادیم، نتیجه به این فانکشن برگشت داده میشه
function calculateDiscount(message, data) {
    if (data.result) {
        // اینپوت رو از حالت فوکس خارج میکنیم
        $('#discount-code-box-in-payment input').blur();
        //نمایش دکمه ضربدر
        $('#remove-discount-code-button-in-payment').removeClass('d-none');
        // مخفی کردن دکمه ثبت کد تخفیف
        $('#add-discount-code-button-in-payment').addClass('d-none');
        // نمایش متن  "کد تخفیف اعمال شد" در پایین اینپوت کد تخفیف
        $('#discount-code-added-text-in-payment').removeClass('d-none');
        // نمایش مقدار تخفیف در بخش چپ صفحه
        $('#show-discount-code-box-in-payment').removeClass('d-none');
        // نمایش مقدار کد تخفیف به صورت فارسی
        $('#discount-code-price-box-in-payment').html(data.discountPrice.toString().addCommaToDigits().toPersinaDigit());

        // محاسبه مجدد قیمت ها بعد از اضافه شدن مقدار کد تخفیف
        calculatePrices(data.discountPrice);
    } else {
        // اینپوت رو دوباره به حالت فوکس در میاریم
        $('#discount-code-box-in-payment input').focus();
    }
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

    // در قسمت قابل پرداخت مقدار متغیر بالایی رو نمایش میدیم
    $('#final-price-box-in-payment')
        .html(finalPriceWithDiscountCodePrice.toString().addCommaToDigits().toPersinaDigit());

    // اگر بخش "سود شما از خرید" به دلیل نبود تخفیف مخفی بود، اون رو نمایش میدیم
    // که میزان کد تخفیف رو در اونجا نمایش بدیم
    $('#discount-price-box-in-payment').removeClass('d-none');
    // محاسبه مجدد میزان تخفیف کاربر
    // تخفیف خود کالا ها به علاوه مقدار کد تخفیف
    $('#discount-price-box-in-payment span:last')
        .html((discountPrice + discountCodePrice).toString().addCommaToDigits().toPersinaDigit());
}

// همینکه روی دکمه پرداخت کلیک بشه، دکمه رو غیر فعال کنیم که مجددا روی دکمه کلیک نشه
$('form').submit(function() {
    $('#create-order-and-pay-button').attr('disabled', 'disabled');
});