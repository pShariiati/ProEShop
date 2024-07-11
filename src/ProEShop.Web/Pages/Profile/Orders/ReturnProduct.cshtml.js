$(function () {
    // کلیک روی چکباکس محصولات
    $('#product-items-in-return-products input:checkbox').change(function () {
        // آیا حداقل یکی از چکباکس های محصولات چک خورده است
        if ($('#product-items-in-return-products input:checkbox:checked').length) {
            // دکمه ادامه فعال شود
            $('#continue-button-in-return-product').removeAttr('disabled');
        } else {
            // دکمه ادامه غیر فعال شود
            $('#continue-button-in-return-product').attr('disabled', 'disabled');
        }
    });

    // کلیک روی آیتم های محصولات مرجوعی
    $('.return-product-item-box-in-return-product').click(function (e) {
        // اگر روی خود چک باکس یا عکس کلیک شد کاری نکن
        var target = $(e.target);
        if (target.is(':checkbox') || target.is('img')) {
            return;
        }

        // المنت چکباکس
        var checkbox = $(this).find('input:checkbox');
        // اگه تیک خورده بود تیکشو بردار
        // اگه تیک نداشت تیکشو فعال کن
        // trigger('chanage')
        // کد بالا برای این است که رخداد
        // change
        // چکباکس فراخوانی شود
        checkbox.prop('checked', !checkbox.prop('checked')).trigger('change');
    });
});

// بعد از اینکه کالا های مورد نظر رو جهت مرجوعی انتخاب کرد و روی ادامه کلیک کرد و از سمت سرور هم برگشت
// این فانکشن فراخوانی می شود
function returnProductSuccessful(message, data) {
    showToastr('success', message);
    // رفتن به صفحه جزییات محصولات مرجوعی
    // در صفحه جزییات دلیل بازگشت کالا و ... وارد میشود
    location.href = `/Profile/Orders/ReturnProductDetails/${data.returnProductId}`;
}