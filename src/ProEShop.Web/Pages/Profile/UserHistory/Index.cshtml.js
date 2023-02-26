$(function () {
    // کلیک روی محصولات
    $('.product-item-in-user-history').click(function (e) {
        // روی کدوم المنت کلیک کرده
        var selectedEl = $(e.target);

        // آیا روی دکمه حذف کردن کلیک کرده
        if (selectedEl.parent().hasClass('remove-product-in-user-history')
            || selectedEl.hasClass('remove-product-in-user-history')) {
            e.preventDefault();

            var productTitle = $(this).find('.product-title-in-user-history').text().trim();
            var productId = $(this).attr('product-id');

            var removeProductModal = $('#remove-product-in-user-history-modal');
            removeProductModal.find('span').html(productTitle);
            removeProductModal.find('input:hidden:first').val(productId);
            removeProductModal.modal('show');
        }
    });
});

// حذف کردن محصول از بازدید ها اخیر
function removeProductFormUserHistory(message, data) {
    // حذف محصول از
    // UI
    $('.product-item-in-user-history[product-id="' + data.productId + '"]').remove();
    showToastr('success', message);
}