$(function () {
    // >
    // علامت بالا در سلکتور، به معنی اشاره کردن به فرزندان مستقیم می باشد
    if ($('#products-box-in-checkout-page > div').length > 1) {
        $('#multiple-shipping-in-checkout-page').removeClass('d-none');
    }

    // موقعی دکمه "اِن مرسوله" هاور شد
    // دراپ دان منو رو نشون بده
    // موقعی که بلر شد مخفیش کن
    $('#shipping-dropdown').hover(function () {
        $(this).dropdown('show');
    },
        function () {
            $(this).dropdown('hide');
        });
});