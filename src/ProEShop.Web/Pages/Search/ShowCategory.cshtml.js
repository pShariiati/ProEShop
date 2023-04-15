$(function () {
    // جستجو روی برند ها
    $('#search-on-brands-input-in-search-on-category').keyup(function () {
        var selectedValue = this.value;
        // تمامی برند هارو نمایش بده
        $('#brands-box-in-search-on-category .list-item-in-search-on-category').removeClass('d-none');
        // دکمه ضربدر رو مخفی کن
        $('#close-button-for-brands-search-in-search-on-category').addClass('d-none');
        if (!isNullOrWhitespace(selectedValue)) {
            // دکمه ضربدر رو نشون بده
            $('#close-button-for-brands-search-in-search-on-category').removeClass('d-none');
            // همه برند ها رو مخفی کن به جز اونیکه روش سرچ انجام شده
            $('#brands-box-in-search-on-category .list-item-in-search-on-category')
                .not('[brand-full-title*="' + selectedValue.toLowerCase().trim() + '"]')
                .addClass('d-none');
        }
    });
});

// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPagination(el) {
    // اگر در داخل صفحه یک هستیم، و دوباره روی صفحه یک کلیک کردیم نیازی به گرفتن
    // اطلاعات از سمت سرور نیست و نباید اجازه دهیم درخواستی به سمت سرور زده شود
    if ($(el).hasClass('bg-danger')) {
        return;
    }

    var pageNumber = $(el).attr('page-number');

    var dataToSend = {
        pageNumber: pageNumber,
        brands: getSelectedBrands()
    }

    getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
}

// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPaginationFunction(data) {
    $('#products-box-in-search-on-category').html(data);
    convertEnglishNumbersToPersianNumber();
}

// اگه چکباکس برند ها تیکش فعال یا غیر فعال شد
// این ایونت فراخوانی میشه
$('#brands-box-in-search-on-category input:checkbox').change(function () {
    // پیج نامبر رو نمیفرستیم که صفحه یک برو برای ما در نظر بگیره
    var dataToSend = {
        brands: getSelectedBrands()
    }

    // نام برند هایی که تیک آنها فعال شده
    var selectedBrands = [];
    dataToSend.brands.forEach(item => {
        selectedBrands.push($('#brands-box-in-search-on-category label[for="brand-in-search-on-category-' + item + '"] strong').text());
    });

    var brandsEl = $('#selected-brands-in-search-on-category');
    brandsEl.html('');
    selectedBrands.forEach(item => {
        brandsEl.append(item + "، ");
    });
    // حذف کاما و فاصله آخر متن
    brandsEl.text(brandsEl.text().replace(/،\s$/, ''));

    getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
});

// آیدی برند های انتخاب شده رو برگشت میزنه
function getSelectedBrands() {
    var activeBrands = [];
    $('#brands-box-in-search-on-category input:checked').each(function () {
        activeBrands.push($(this).val());
    });
    return activeBrands;
}