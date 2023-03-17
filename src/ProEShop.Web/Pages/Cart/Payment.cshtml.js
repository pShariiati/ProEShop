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
    $('#show-discount-box-el-in-payment').click();

    // اگر روی دکمه ثبت کد تخفیف کلیک شد، کد تخفیف رو به سمت سرور میفرسته که بررسی کنه همچین کدی وجود داره یا نه
    $('#discount-code-box-in-payment button').click(function() {
        var code = $(this).parent().find('input').val();
        getDataWithAJAX('?handler=CheckForDiscount', { code }, 'calculateDiscount');
    });
});

// بعد از اینکه کد تخیف رو به سمت سرور فرستادیم، نتیجه به این فانکشن برگشت داده میشه
function calculateDiscount(message, data) {
    console.log(data);
}

// همینکه روی دکمه پرداخت کلیک بشه، دکمه رو غیر فعال کنیم که مجددا روی دکمه کلیک نشه
$('form').submit(function() {
    $('#create-order-and-pay-button').attr('disabled', 'disabled');
});