// نمایش نظرات به صورت صفحه بندی شده
function showCommentsByPagination(el) {
    // اگر در داخل صفحه یک هستیم، و دوباره روی صفحه یک کلیک کردیم نیازی به گرفتن
    // اطلاعات از سمت سرور نیست و نباید اجازه دهیم درخواستی به سمت سرور زده شود
    if ($(el).hasClass('bg-danger')) {
        return;
    }

    var pageNumber = $(el).attr('page-number');

    getHtmlWithAJAX('?handler=ShowCommentsByPagination', { pageNumber: pageNumber}, 'showCommentsByPaginationFunction');
}

// نمایش نظرات به صورت صفحه بندی شده
function showCommentsByPaginationFunction(data) {
    $('#comments-box-in-comment-profile').html(data);
    convertEnglishNumbersToPersianNumber();

    scrollToEl('body');
}

// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPagination(el) {
    // اگر در داخل صفحه یک هستیم، و دوباره روی صفحه یک کلیک کردیم نیازی به گرفتن
    // اطلاعات از سمت سرور نیست و نباید اجازه دهیم درخواستی به سمت سرور زده شود
    if ($(el).hasClass('bg-danger')) {
        return;
    }

    var pageNumber = $(el).attr('page-number');

    getHtmlWithAJAX('?handler=ShowProductsByPagination', { pageNumber: pageNumber }, 'showCommentsByPaginationFunction');
}

// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPaginationFunction(data) {
    $('#comments-box-in-comment-profile').html(data);

    scrollToEl('body');
}