$(document).ready(function () {
    var win = $(this);
    var mainInputEl = $('#main-search-input');
    if (win.width() > 456) {
        mainInputEl.attr('placeholder', 'نام کالا، برند یا دسته مورد نظر خود را جستجو نمایید ...');
    } else {
        mainInputEl.attr('placeholder', 'جستجو ...');
    }
    $(window).on('resize', function () {
        win = $(this);
        if (win.width() > 456) {
            mainInputEl.attr('placeholder', 'نام کالا، برند یا دسته مورد نظر خود را جستجو نمایید ...');
        } else {
            mainInputEl.attr('placeholder', 'جستجو ...');
        }
    });
});