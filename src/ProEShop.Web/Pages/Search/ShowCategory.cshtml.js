// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPagination(el) {
    // اگر در داخل صفحه یک هستیم، و دوباره روی صفحه یک کلیک کردیم نیازی به گرفتن
    // اطلاعات از سمت سرور نیست و نباید اجازه دهیم درخواستی به سمت سرور زده شود
    if ($(el).hasClass('bg-danger')) {
        return;
    }

    var pageNumber = $(el).attr('page-number');

    var dataToSend = {
        pageNumber: pageNumber
    }

    getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
}

// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPaginationFunction(data) {
    $('#products-box-in-search-on-category').html(data);
    convertEnglishNumbersToPersianNumber();
}