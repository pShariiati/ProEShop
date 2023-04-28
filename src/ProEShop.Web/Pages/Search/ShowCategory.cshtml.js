$(function () {
    // جستجو در اینپوت ها
    $('.search-input-in-search-on-category').keyup(function () {
        var selectedValue = this.value;
        // تمامی آیتم ها رو نمایش بده
        $(this).parents('.list-in-search-on-category').find('.list-item-in-search-on-category').removeClass('d-none');
        // آیکون ضربدر رو مخفی کن
        $(this).next('i').addClass('d-none');
        // آیکون سرچ رو نمایش بده
        $(this).prev('i').removeClass('d-none');
        if (!isNullOrWhitespace(selectedValue)) {
            // دکمه ضربدر رو نشون بده
            $(this).next('i').removeClass('d-none');
            // آیکون سرچ رو مخفی کن
            $(this).prev('i').addClass('d-none');
            // همه آیتم ها مخفی بشن به جز اونایی که براشون سرچ انجام شده
            $(this).parents('.list-in-search-on-category').find('.list-item-in-search-on-category')
                .not('[title-to-search*="' + selectedValue.toLowerCase().trim() + '"]')
                .addClass('d-none');
        }
    });

    // موقعی که روی آیکون ضربدر کلیک شد، اینپوت رو خالی کن
    $('.input-clear-icon-in-search-on-category').click(function () {
        // مخفی کردن آیکون ضربدر
        $(this).addClass('d-none');
        // خالی کردن اینپوت
        $(this).prev('input').val('');
        // نمایش آیکون سرچ
        $(this).parent().find('i:first').removeClass('d-none');
        $(this).parents('.list-in-search-on-category').find('.list-item-in-search-on-category').removeClass('d-none');
    });

    // محدوده قیمت
    // از قیمت
    $('#from-price-input-in-search-on-category').on('keypress', function (event) {
        return keypressForPersianNumbersInPriceInput(event, this, true, 13);
    }).on('keydown', function (event) {
        return backspaceAndDeleteForPersianNumbersInPriceInput(event, this, true);
    }).on('paste drop', function () {
        customEventsForPersianNumbersInPriceInput(this, true, 13);
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
        brands: getSelectedBrands(),
        variants: getSelectedVariants
    }

    // نام برند هایی که تیک آنها فعال شده
    var selectedBrands = [];
    dataToSend.brands.forEach(item => {
        selectedBrands.push($('#brands-box-in-search-on-category label[for="brand-in-search-on-category-' + item + '"] strong').text());
    });

    var brandsEl = $('#selected-brands-in-search-on-category');
    brandsEl.removeClass('d-none');
    brandsEl.html('');
    selectedBrands.forEach(item => {
        brandsEl.append(item + "، ");
    });
    // حذف کاما و فاصله آخر متن
    brandsEl.text(brandsEl.text().replace(/،\s$/, ''));

    // کدوم چکباکس انتخاب شده ؟ اونو یا به قسمت انتخاب شما
    // اضافه میکنیم یا اگه تیک چکباکس غیر فعال شده باشه، اونو از قسمت انتخاب شما حذف میکنیم

    var currentDiv = $(this).parents('div');
    var divClass = currentDiv.attr('class');
    var cssIsolationString;
    currentDiv.each(function () {
        $.each(this.attributes, function () {
            if (this.name.startsWith('b-')) {
                cssIsolationString = this.name;
                return false;
            }
        });
    });

    // دیو موارد انتخاب شده
    var selectedItemsEl = currentDiv.parents('.list-in-search-on-category').find('.selected-items-in-search-on-category');
    if (this.checked) {
        selectedItemsEl.removeClass('d-none');
        selectedItemsEl.find('> div:eq(1)')
            .prepend(
                `<div ${cssIsolationString} class="${divClass}">${currentDiv.html()}</div>`
            );
        selectedItemsEl.find('input:first').attr('checked', 'checked');
    } else {
        var checkboxValue = this.value;
        selectedItemsEl.find('input[value="' + checkboxValue + '"]')
            .parents('div:first')
            .remove();
    }
    // اگر رکوردی وجود نداشت بخش انتخاب شما رو مخفی میکنیم
    if (!selectedBrands.length) {
        selectedItemsEl.addClass('d-none');
        brandsEl.addClass('d-none');
    }
    getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
});

// اگه چکباکس تنوع ها تیکش فعال یا غیر فعال شد
// این ایونت فراخوانی میشه
$('#colors-and-sizes-box-in-search-on-category input:checkbox').change(function () {
    // پیج نامبر رو نمیفرستیم که صفحه یک برو برای ما در نظر بگیره
    var dataToSend = {
        brands: getSelectedBrands(),
        variants: getSelectedVariants()
    }

    // نام تنوع هایی که تیک آنها فعال شده
    var selectedVariants = [];
    dataToSend.brands.forEach(item => {
        selectedVariants.push($('#colors-and-sizes-box-in-search-on-category label[for="colors-and-sizes-in-search-on-category-' + item + '"] strong').text());
    });

    // کدوم چکباکس انتخاب شده ؟ اونو یا به قسمت انتخاب شما
    // اضافه میکنیم یا اگه تیک چکباکس غیر فعال شده باشه، اونو از قسمت انتخاب شما حذف میکنیم

    var currentDiv = $(this).parents('div');
    var divClass = currentDiv.attr('class');
    var cssIsolationString;
    currentDiv.each(function () {
        $.each(this.attributes, function () {
            if (this.name.startsWith('b-')) {
                cssIsolationString = this.name;
                return false;
            }
        });
    });

    // دیو موارد انتخاب شده
    var selectedItemsEl = currentDiv.parents('.list-in-search-on-category').find('.selected-items-in-search-on-category');
    if (this.checked) {
        selectedItemsEl.removeClass('d-none');
        selectedItemsEl.find('> div:eq(1)')
            .prepend(
                `<div ${cssIsolationString} class="${divClass}">${currentDiv.html()}</div>`
            );
        selectedItemsEl.find('input:first').attr('checked', 'checked');
    } else {
        var checkboxValue = this.value;
        selectedItemsEl.find('input[value="' + checkboxValue + '"]')
            .parents('div:first')
            .remove();
    }
    // اگر رکوردی وجود نداشت بخش انتخاب شما رو مخفی میکنیم
    if (!selectedVariants.length) {
        selectedItemsEl.addClass('d-none');
    }
    getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
});

// اگر روی چکباکس موارد انتخاب شده کلیک شد باید اونارو حذف کنیم
$(document).on('change', '.selected-items-in-search-on-category input', function () {
    var currentValue = this.value;
    $(this).parents('.selected-items-in-search-on-category').next().find('input[value="' + currentValue + '"]')
        .trigger('click');
});

// آیدی برند های انتخاب شده رو برگشت میزنه
function getSelectedBrands() {
    var activeBrands = [];
    $('#brands-box-in-search-on-category input:checked').each(function () {
        activeBrands.push($(this).val());
    });
    return activeBrands;
}

// آیدی تنوع های انتخاب شده رو برگشت میزنه
function getSelectedVariants() {
    var activeVariants = [];
    $('#colors-and-sizes-box-in-search-on-category input:checked').each(function () {
        activeVariants.push($(this).val());
    });
    return activeVariants;
}