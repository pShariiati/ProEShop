﻿$(function () {

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

$('form').submit(function() {
    $('#create-order-and-pay-button').attr('disabled', 'disabled');
});