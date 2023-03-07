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

    // logout
    $('#log-out-div-in-profile').click(function() {
        $(this).find('form').submit();
    });

    // اکتیو کردن لینک جاری در بخش سایدبار
    var currentPathname = location.pathname.toLowerCase().replace(/\/index$/, '');

    $('#sidebar-links-in-profile a[lowercase-url="' + currentPathname + '"]').find('div')
        .removeClass('d-none');
});