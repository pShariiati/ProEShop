function addProductVariantToCart(message, data) {
    $('#cart-body').html(data.cartBody);

    // چون محتویات صفحه دوباره لود میشن
    // تول تیپ ها از کار میافتن، به خاطر همین باید متد فعال سازی
    // اونارو یکبار دیگه کال کنیم
    enablingNormalTooltips();

    // متن المنت هایی که کلاس
    // persian-numbers
    // دارن رو به عدد فارسی تغییر میدیم
    $('#cart-body .persian-numbers').each(function () {
        var text = $(this).html();
        $(this).html(text.toPersinaDigit());
    });

    // تعداد آیتم های داخل سبد خرید
    var allProductsCountInCart = $('#cart-page-title span').html();

    // اگر پارشل سبدِ خریدِ خالی، نمایش داده شود
    // باید در بخش سبد خریدِ هدر، عدد صفر نمایش بدیم
    if (allProductsCountInCart) {
        $('#cart-count-text').html(allProductsCountInCart);
    } else {
        $('#cart-count-text').html('۰');
    }
}

$(function () {

    // موقعی که روی دکمه های به علاوه، منفی و سطل آشعال کلیک شد
    // فرم باید سمت سرور ارسال شود
    $(document).on('click', '.increaseProductVariantInCartButton, .decreaseProductVariantInCartButton, .empty-variants-in-cart',
        function () {
            if ($(this).parents('span').hasClass('text-custom-grey')) {
                return;
            }
            $(this).parent().submit();
        });
});