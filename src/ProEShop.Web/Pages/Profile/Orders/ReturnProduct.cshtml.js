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
});

// بعد از اینکه کالا های مورد نظر رو جهت مرجوعی انتخاب کرد و روی ادامه کلیک کرد و از سمت سرور هم برگشت
// این فانکشن فراخوانی می شود
function returnProductSuccessful(message, data) {
    showToastr('success', message);
}