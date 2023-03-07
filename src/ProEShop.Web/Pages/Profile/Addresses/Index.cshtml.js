// چرا این متغیر ها رو به صورت گلوبال تعریف کردیم ؟
// چون این متغیر ها داخل داکیومنت ردی مقدار دهی میشن
// و نمیشه خارج از داکیومنت ردی بهشون دسترسی داشت
// پس به صورت گلوبال تعریف میکنیم که بتونیم خارج از داکیومنت ردی هم
// بهشون دسترسی داشته باشیم
var deleteAddressForm;
var deleteAddressEl;

$(function () {
    // حذف آدرس
    $('.delete-address-in-profile').click(function () {
        // فرم رو میگیریم که بتونیم بعد زدن دکمه تایید فرم رو ارسال کنیم
        deleteAddressForm = $(this).find('form');
        // المنت آدرس رو میگیریم که بتونیم بعد عملیات سمت سرور حذفش کنیم
        deleteAddressEl = $(this).parents('.address-item-profile-address');
        showSweetAlert2('آیا از حذف این آدرس از لیست آدرس ها اطمینان دارید؟', 'deleteAddress');
    });
});

// بعد زدن دکمه ی تاییدِ حذفِ آدرس
// این فانکشن فراخوانی میشود
function deleteAddress() {
    deleteAddressForm.submit();
}

// بعد از حذف آدرس از سرور این فانشکن فراخوانی میشود
function deleteAddressFunction(message) {
    showToastr('success', message);

    // حذف کردن المنت آدرس
    deleteAddressEl.remove();
}