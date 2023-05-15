// بیشترین قیمت محصولِ این دسته بندی
var currentCategoryMaximumPrice = parseInt($('#prices-range-in-search-on-category').attr('maximum-price'));
// کمترین قیمت محصولِ این دسته بندی
var currentCategoryMinimumPrice = parseInt($('#prices-range-in-search-on-category').attr('minimum-price'));

// موقعی که زبان رو  عوض کنیم رخداد
// Keyup
// اجرا میشه، پس برای اینکه این مشکل رو حل کنیم مقدار داخل اینپوت رو بررسی میکنیم، اگه تغییر پیدا نکرده باشه درخواستی هم
// به سمت سرور ارسال نمیکنیم، چون با تغییر زبان، مقدار داخل اینپوت تغییر پیدا نمیکند
var currentMinimumPriceValueToCheck = $('#from-price-input-in-search-on-category').val();
var currentMaximumPriceValueToCheck = $('#up-to-price-input-in-search-on-category').val();

$(function () {
    // اگه چکباکس برند ها تیکش فعال یا غیر فعال شد
    // این ایونت فراخوانی میشه
    $('#brands-box-in-search-on-category input:checkbox').change(function () {
        // پیج نامبر رو نمیفرستیم که صفحه یک برو برای ما در نظر بگیره
        var dataToSend = getDataToSend();

        // نام برند هایی که تیک آنها فعال شده
        var selectedBrands = [];
        dataToSend.brands.forEach(item => {
            selectedBrands.push($('#brands-box-in-search-on-category label[for="brand-in-search-on-category-' + item + '"] strong').text());
        });

        // دیو موارد انتخاب شده
        var brandsEl = $('#selected-brands-in-search-on-category');
        brandsEl.removeClass('d-none');
        brandsEl.html('');
        selectedBrands.forEach(item => {
            brandsEl.append(item + "، ");
        });
        // حذف کاما و فاصله آخر متن
        brandsEl.text(brandsEl.text().replace(/،\s$/, ''));

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
        // کدوم چکباکس انتخاب شده ؟ اونو یا به قسمت انتخاب شما
        // اضافه میکنیم یا اگه تیک چکباکس غیر فعال شده باشه، اونو از قسمت انتخاب شما حذف میکنیم
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
        var dataToSend = getDataToSend();

        // نام تنوع هایی که تیک آنها فعال شده
        var selectedVariants = [];
        dataToSend.variants.forEach(item => {
            selectedVariants.push($('#colors-and-sizes-box-in-search-on-category label[for="colors-and-sizes-in-search-on-category-' + item + '"] strong').text());
        });

        // دیو موارد انتخاب شده
        var variantsEl = $('#selected-variants-in-search-on-category');
        variantsEl.removeClass('d-none');
        variantsEl.html('');
        selectedVariants.forEach(item => {
            variantsEl.append(item + "، ");
        });
        // حذف کاما و فاصله آخر متن
        variantsEl.text(variantsEl.text().replace(/،\s$/, ''));

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
        // کدوم چکباکس انتخاب شده ؟ اونو یا به قسمت انتخاب شما
        // اضافه میکنیم یا اگه تیک چکباکس غیر فعال شده باشه، اونو از قسمت انتخاب شما حذف میکنیم
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
            variantsEl.addClass('d-none');
        }
        getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
    });

    // چکباکس مربوط به فقط کالاهای موجود
    $('#only-exist-products-in-search-on-category').change(function () {
        getHtmlWithAJAX('?handler=ShowProductsByPagination', getDataToSend(), 'showProductsByPaginationFunction');
    });

    // اگر روی چکباکس موارد انتخاب شده کلیک شد باید اونارو حذف کنیم
    $(document).on('change', '.selected-items-in-search-on-category input', function () {
        var currentValue = this.value;
        $(this).parents('.selected-items-in-search-on-category').next().find('input[value="' + currentValue + '"]')
            .trigger('click');
    });

    // چینج شدن چکباکس فیچر ها
    $('.features-in-search-on-category input').change(function () {
        // نام برند هایی که تیک آنها فعال شده
        var selectedFeatures = [];
        $(this).parents('.all-items-box-in-search-on-category').find('input:checked').each(function() {
            selectedFeatures.push($(this).val());
        });

        // دیو موارد انتخاب شده
        var featuresEl = $(this).parents('.features-in-search-on-category').find('.selected-features-text-in-search-category');
        featuresEl.removeClass('d-none');
        featuresEl.html('');
        selectedFeatures.forEach(item => {
            featuresEl.append(item + "، ");
        });
        // حذف کاما و فاصله آخر متن
        featuresEl.text(featuresEl.text().replace(/،\s$/, ''));

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

        // کدوم چکباکس انتخاب شده ؟ اونو یا به قسمت انتخاب شما
        // اضافه میکنیم یا اگه تیک چکباکس غیر فعال شده باشه، اونو از قسمت انتخاب شما حذف میکنیم
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
        if (!selectedFeatures.length) {
            selectedItemsEl.addClass('d-none');
            featuresEl.addClass('d-none');
        }
        getHtmlWithAJAX('?handler=ShowProductsByPagination', getDataToSend(), 'showProductsByPaginationFunction');
    });

    // جستجو در اینپوت ها
    $('.search-input-in-search-on-category').keyup(function () {
        var selectedValue = this.value;
        // تمامی آیتم ها رو نمایش بده
        $(this).parents('.list-in-search-on-category').find('.all-items-box-in-search-on-category .list-item-in-search-on-category').removeClass('d-none');
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
            $(this).parents('.list-in-search-on-category').find('.all-items-box-in-search-on-category .list-item-in-search-on-category')
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

    var globalTimeout = null;
    // محدوده قیمت
    // از قیمت
    $('#from-price-input-in-search-on-category').on('keypress', function (event) {
        return keypressForPersianNumbersInPriceInput(event, this, true, 13);
    }).on('keydown', function (event) {
        return backspaceAndDeleteForPersianNumbersInPriceInput(event, this, true);
    }).on('paste drop', function () {
        customEventsForPersianNumbersInPriceInput(this, true, 13);
    }).on('keyup',function() {
        if (globalTimeout != null) {
            clearTimeout(globalTimeout);
        }
        globalTimeout = setTimeout(function () {
            var currentValue = $('#from-price-input-in-search-on-category').val();
            // توضیحات در بخش متغیر
            if (currentValue === currentMinimumPriceValueToCheck) {
                return;
            }
            currentMinimumPriceValueToCheck = currentValue;
            getHtmlWithAJAX('?handler=ShowProductsByPagination', getDataToSend(), 'showProductsByPaginationFunction');
        }, 500);
    });

    // محدوده قیمت
    // تا قیمت
    $('#up-to-price-input-in-search-on-category').on('keypress', function (event) {
        return keypressForPersianNumbersInPriceInput(event, this, true, 13);
    }).on('keydown', function (event) {
        return backspaceAndDeleteForPersianNumbersInPriceInput(event, this, true);
    }).on('paste drop', function () {
        customEventsForPersianNumbersInPriceInput(this, true, 13);
    }).on('keyup', function () {
        if (globalTimeout != null) {
            clearTimeout(globalTimeout);
        }
        globalTimeout = setTimeout(function () {
            var currentValue = $('#up-to-price-input-in-search-on-category').val();
            // توضیحات در بخش متغیر
            if (currentValue === currentMaximumPriceValueToCheck) {
                return;
            }
            currentMaximumPriceValueToCheck = currentValue;
            getHtmlWithAJAX('?handler=ShowProductsByPagination', getDataToSend(), 'showProductsByPaginationFunction');
        }, 500);
    });

    // range slider for price
    var rangeSlider = document.getElementById('prices-range-in-search-on-category');
    noUiSlider.create(rangeSlider, {
        connect: true,
        step: currentCategoryMaximumPrice / 100,
        direction: 'rtl',
        start: [0, currentCategoryMaximumPrice],
        range: {
            'min': 0,
            'max': currentCategoryMaximumPrice
        }
    });

    rangeSlider.noUiSlider.on('update', function (values, handle) {
        var value = Math.round(values[handle]);
        if (handle) {
            $('#up-to-price-input-in-search-on-category').val(value.toString().addCommaToDigits().toPersinaDigit());
        } else {
            $('#from-price-input-in-search-on-category').val(value.toString().addCommaToDigits().toPersinaDigit());
        }
    });

    rangeSlider.noUiSlider.on('change', function (values, handle) {
        if (handle) {
            $('#up-to-price-input-in-search-on-category').keyup();
        } else {
            $('#from-price-input-in-search-on-category').keyup();
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

    var dataToSend = getDataToSend();
    dataToSend.pageNumber = pageNumber;

    getHtmlWithAJAX('?handler=ShowProductsByPagination', dataToSend, 'showProductsByPaginationFunction');
}

// نمایش محصولات به صورت صفحه بندی شده
function showProductsByPaginationFunction(data) {
    $('#products-box-in-search-on-category').html(data);
    convertEnglishNumbersToPersianNumber();
}

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

// گرفتن مقدار داخل اینپوت های کمترین قیمت و بیشترین قیمت
function getMinAndMaxPrice(isMin) {
    var result = parseInt($(`#${isMin ? 'from' : 'up-to'}-price-input-in-search-on-category`).val().replace(/\,/g, '')
        .toEnglishDigit());
    if (currentCategoryMaximumPrice === result) {
        return 0;
    }
    return result;
}

// گرفتن مقدار فیچر هایی که تیکشون فعال شده
// feature id + ___ + values (split by |||)
function getActiveFeatures() {
    var activeFeatures = [];
    $('.features-in-search-on-category').each(function() {
        var featureId = $(this).attr('feature-id');
        var currentFeatureInputs = $(this).find('input:checked');
        if (currentFeatureInputs.length) {
            var featureTextToAdd = featureId + '___';
            currentFeatureInputs.each(function() {
                var featureValue = this.value;
                featureTextToAdd += featureValue + '|||';
            });
            activeFeatures.push(featureTextToAdd);
        }
    });
    return activeFeatures;
}

// گرفتن تمام دیتا های فیلتر ها که به سمت سرور ارسالشون کنیم
function getDataToSend() {
    return {
        brands: getSelectedBrands(),
        variants: getSelectedVariants(),
        minimumPrice: getMinAndMaxPrice(true),
        maximumPrice: getMinAndMaxPrice(false),
        onlyExistsProducts: $('#only-exist-products-in-search-on-category').prop('checked'),
        features: getActiveFeatures()
    }
}