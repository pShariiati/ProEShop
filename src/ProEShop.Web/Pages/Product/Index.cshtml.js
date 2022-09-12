function addProductVariantToCart(message, data) {
    // المنت افزودن به سبد خرید
    var addProductVariantToCartEl = $('.add-product-variant-to-cart[variant-id="' + data.productVariantId + '"]');

    // المنت بخش سبد خرید
    var cartSectionEl = $('.product-variant-in-cart-section[variant-id="' + data.productVariantId + '"]');

    // تعدادی که از سمت سرور اومده رو به کاربر نشون میدیم
    cartSectionEl.find('.product-variant-count-in-cart span:first').html(data.count.toString().toPersinaDigit());

    // اگر سبد خرید پر بود
    // متن حداکثر باید نشون داده بشه
    // رنگ دکمه پلاس خاکستری بشه و کرسر هم باید دیفالت شه
    if (data.isCartFull) {
        cartSectionEl.find('.product-variant-count-in-cart span:last').removeClass('d-none');
        cartSectionEl.find('.increaseProductVariantInCartButton').parents('span').addClass('text-custom-grey');
        cartSectionEl.find('.increaseProductVariantInCartButton').parents('span').removeClass('pointer-cursor');
    } else {
        cartSectionEl.find('.product-variant-count-in-cart span:last').addClass('d-none');
        cartSectionEl.find('.increaseProductVariantInCartButton').parents('span').removeClass('text-custom-grey');
        cartSectionEl.find('.increaseProductVariantInCartButton').parents('span').addClass('pointer-cursor');
    }

    // کدام تنوع این محصول انتخاب شده است
    // آیدی تنوع محصول انتخاب شده رو می گیریم

    // No variant
    var selectedProductVariantId = 0;
    // Product variant id: Color
    var selectedColor = $('#product-variants-box-in-show-product-info div i').not('[class*="d-none"]')
        .parents('div').attr('product-variant-id');
    // Product variant id: Size
    var selectedSize = $('#product-variants-box-in-show-product-info select').find(':selected')
        .attr('product-variant-id');
    // اگر این محصول تنوعش سایز یا رنگ بود وارد ایف میشه
    if (selectedColor || selectedSize) {
        selectedProductVariantId = parseInt(
            selectedColor || selectedSize
        );
    } else {
        // اگر این محصول تنوع نداشته باشه مثل ماسک، وارد الس میشه
        // البته که میشه این الس رو کلا ننوشت و متغیر
        // selectedProductVariantId
        // در چند لاین بالاتر رو مساوی
        // data.productVariantId
        // قرار داد، چون داخل این الس هم دقیقا همین کار انجام میشه
        selectedProductVariantId = data.productVariantId;
    }

    // چرا از این ایف استفاده کرده ایم ؟
    // برای مثال من الان در بخش تنوع های محصول در داخل رنگ آبی هستم و رنگ انتخابی آبی است
    // حالا در بخش سبد خرید هدر سایت، تعداد رنگ قرمز این محصول رو افزایش میدم
    // اگه این ایف نباشه کارت سکشن رنگ قرمز رو هم کنار کارت سکشن آبی نشون میده
    // با این ایف بررسی میکنیم که در صورتی، کارت سکشن قرمز رو نشون بده
    // که رنگ انتخابی این محصول، روی رنگ قرمز باشد نه رنگ دیگری
    if (selectedProductVariantId === data.productVariantId) {

        // بخش افزودن به سبد خرید و کارت سکشن رو مخفی میکنیم
        cartSectionEl.addClass('d-none');
        addProductVariantToCartEl.addClass('d-none');

        // اگه تعداد موجود در سبد خرید بیشتر از یک باشد
        // باید دکمه کارت سکشن رو نشون بدیم
        if (data.count > 0) {
            cartSectionEl.removeClass('d-none');
        } else {
            // اگر تعداد داخل سبد خرید صفر باشد باید دکمه افزودن به سبد خرید رو نشون بدیم
            addProductVariantToCartEl.removeClass('d-none');
        }
    }

    // اگر تعداد یک بود باید آیکون
    // Trash
    // رو نشون بدیم و علامت منفی رو مخفی کنیم
    if (data.count === 1) {
        cartSectionEl.find('.decreaseProductVariantInCartButton').parents('span').addClass('d-none');
        cartSectionEl.find('.empty-variants-in-cart').parents('span').removeClass('d-none');
    } else if (data.count > 1) {
        // اگر تعداد بیشتر از یک بود در اون صورت علامت آیکون
        // Trash
        // رو مخفی میکنیم و علامت منفی رو نشون میدیم
        cartSectionEl.find('.decreaseProductVariantInCartButton').parents('span').removeClass('d-none');
        cartSectionEl.find('.empty-variants-in-cart').parents('span').addClass('d-none');
    }

    // اچ تی ام ال برگشتی از سمت سرور که شامل تمامی محصولات داخل
    // سبد خرید این کاربر هست رو داخل این المنت نشون میدیم
    $('#cart-dropdown-body').html(data.cartsDetails);

    // چونکه خروجی صحفه سبد خرید رو با بعدا به صفحه اضافه میکنیم، اعداد انگلیسی
    // هستند، پس با کد پایین اونارو به فارسی تغییر میدم
    $('#cart-dropdown-body .persian-numbers').each(function () {
        var text = $(this).html();
        $(this).html(text.toPersinaDigit());
    });

    // Show count of products in cart using in bottom of cart dropdown
    var allProductsCountInCart = $('#cart-dropdown-body div:first').attr('all-products-count-in-cart');
    $('#cart-count-text').html(allProductsCountInCart.toPersinaDigit());
}

function copyProductLinkToClipboardFunction() {
    var copyButtonSelector = $('#copy-product-link-button');
    var copyButtonHtml = copyButtonSelector.html();
    copyButtonSelector.html('<i class="bi bi-clipboard-check rem20px"></i> کپی شد');

    // این فانکشن فقط یکبار فراخوانی میشود
    // و بهترین گزینه برای این سناریو می باشد
    setTimeout(function () {
        copyButtonSelector.html(copyButtonHtml);
    }, 2000);
}

$(function () {

    // چونکه مقدار داخل دراپ داون سبد خرید هر بار تغییر میکنه و بعد از لود صفحه به صفحه اضافه میشه در نتیجه باید از
    // $(document).on
    // استفاده کنیم
    $(document).on('click', '.increaseProductVariantInCartButton, .decreaseProductVariantInCartButton, .empty-variants-in-cart',
        function () {
            if ($(this).parents('span').hasClass('text-custom-grey')) {
                return;
            }
            $(this).parent().submit();
        });

    $('.count-down-timer-in-other-variants').each(function () {
        var currentEl = $(this);
        var selectorToShow = currentEl.parents('td').find('div:first');
        var selectorToHide = currentEl.parents('td').find('div:eq(1)');
        countDownTimer(currentEl, selectorToShow, selectorToHide);
    });

    $('.count-down-timer').each(function () {
        var currentEl = $(this);
        var variantValue = currentEl.parent().attr('variant-value');

        var selectorToShow = $('.product-price-in-single-page-of-product[variant-value="' + variantValue + '"]');
        var selectorToHide = currentEl.parent();
        var selectorToHide2 = $('.product-final-price-in-single-page-of-product[variant-value="' + variantValue + '"]');
        countDownTimer(currentEl, selectorToShow, selectorToHide, selectorToHide2);
    });

    function countDownTimerFunction(selector, selectorToShow, selectorToHide, selectorToHide2, countDownDate) {
        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        var daysText = `${days} روز و<br />`;
        if (days === 0) {
            daysText = '';
        }

        var result =
            `${daysText}${seconds < 10 ? '0' + seconds : seconds} : ${minutes < 10 ? '0' + minutes : minutes} : ${hours < 10 ? '0' + hours : hours}`;

        selector.html(result.toPersinaDigit());

        // If the count down is finished, write some text
        if (distance < 0) {
            selector.parents('tr').attr('is-discount-active', 'false');
            selectorToShow.removeClass('d-none');
            selectorToHide.addClass('d-none');
            if (selectorToHide2) {
                selectorToHide2.addClass('d-none');
            }
        }
        return distance;
    }

    function countDownTimer(selector, selectorToShow, selectorToHide, selectorToHide2) {

        var endDateTime = selector.html().trim();

        // Set the date we're counting down to
        var countDownDate = new Date(endDateTime).getTime();

        countDownTimerFunction(selector, selectorToShow, selectorToHide, selectorToHide2, countDownDate);

        // Update the count down every 1 second
        var x = setInterval(function () {
            var result = countDownTimerFunction(selector, selectorToShow, selectorToHide, selectorToHide2, countDownDate);
            if (result < 0) {
                clearInterval(x);
            }
        }, 1000);
    }

    $('#other-sellers-count-box').click(function () {
        $('html, body').animate({
            scrollTop: $('#other-sellers-box').offset().top - 20
        }, 1);
    });

    $('#copy-product-link-button').click(function () {
        var productLink = $(this).attr('product-link');
        copyTextToClipboard(productLink, 'copyProductLinkToClipboardFunction');
    });

    var zoomPluginOptions = {
        fillContainer: true,
        zoomPosition: 'original'
    };

    new ImageZoom(document.getElementById('zoom-image-place'), zoomPluginOptions);

    $('#add-product-to-favorite-form').submit(function () {
        if (!isAuthenticated) {
            showFirstLoginModal();
            return false;
        }
    });

    $('#share-product-button').click(function () {
        $('#share-product-modal').modal('show');
    });

    // Hide other sellers box if it has just one item
    if ($('.other-sellers-table:first tbody tr').length === 1) {
        $('#other-sellers-box, #other-sellers-count-box').addClass('d-none');
    }

    $('#show-all-product-features').click(function () {
        $(this).addClass('d-none');
        $('#features-next-to-product-box li').removeClass('d-none');
    });

    // Add border to the first variant
    $('#product-variants-box-in-show-product-info div:first').addClass('selected-variant-in-show-product-info');

    // Add check to the first variant
    $('#product-variants-box-in-show-product-info i:first').removeClass('d-none');

    // Change variants (color)
    $('#product-variants-box-in-show-product-info div').click(function () {
        if ($(this).find('i').hasClass('d-none') === false) {
            return;
        }

        $('#product-variants-box-in-show-product-info div').removeClass('selected-variant-in-show-product-info');
        $('#product-variants-box-in-show-product-info i').addClass('d-none');

        $(this).find('i').removeClass('d-none');
        $(this).addClass('selected-variant-in-show-product-info');

        var selectedVariantValue = $(this).attr('aria-label');
        var selectedProductVariantId = $(this).attr('product-variant-id');

        changeVariant(selectedVariantValue, selectedProductVariantId);
    });

    // Change variants (size)
    $('#product-variants-box-in-show-product-info select').change(function () {
        var selectedVariantValue = this.value;
        var selectedProductVariantId = $(this).find(':selected').attr('product-variant-id');
        changeVariant(selectedVariantValue, selectedProductVariantId);
    });

    function changeVariant(selectedVariantValue, selectedProductVariantId) {
        $('.other-sellers-table').addClass('d-none');

        // Hidden all main final prices, prices and count down boxes
        $('.product-final-price-in-single-page-of-product').addClass('d-none');
        $('.product-price-in-single-page-of-product').addClass('d-none');
        $('.discount-count-down-box').addClass('d-none');

        $('.other-sellers-table[variant-value="' + selectedVariantValue + '"]').removeClass('d-none');

        // Change variant value
        $('#product-variant-value').html(selectedVariantValue);

        // Change product info in left side box
        var selectedSeller = $('.other-sellers-table[variant-value="' + selectedVariantValue + '"] tbody tr:first');

        // If selected variant had discount
        if (selectedSeller.attr('is-discount-active') === 'true') {
            $('.product-final-price-in-single-page-of-product[variant-value="' + selectedVariantValue + '"]')
                .removeClass('d-none');
            $('.discount-count-down-box[variant-value="' + selectedVariantValue + '"]').removeClass('d-none');
        } else {
            $('.product-price-in-single-page-of-product[variant-value="' + selectedVariantValue + '"]')
                .removeClass('d-none');
        }

        // نمایش تعداد باقیمانده تنوع در انبار
        // اگر کوچکتر مساوی سه باشد
        // این بخش رو نشون میدیم
        $('.latest-product-stock-in-inventory').addClass('d-none');
        $('.latest-product-stock-in-inventory[variant-value="' + selectedVariantValue + '"]').removeClass('d-none');

        // Change shop name
        var selectedShopName = selectedSeller.find('td:first').text();
        $('#shop-details-in-single-page-of-product div').html(selectedShopName);

        // Change tooltip value
        // Shop name tooltip
        var tooltip = bootstrap.Tooltip.getInstance('#product-shop-name-tooltip');
        tooltip.setContent({ '.tooltip-inner': `این کالا توسط فروشنده آن، ${selectedShopName.trim()}، قیمت گذاری شده است.` });

        // Change shop logo
        var selectedShopLogo = selectedSeller.find('td:first i').length === 0 ? 'img' : 'i';
        if (selectedShopLogo === 'img') {
            selectedShopLogo = selectedSeller.find('td:first img').attr('src');
            $('#shop-details-in-single-page-of-product i').addClass('d-none');
            $('#shop-details-in-single-page-of-product img').removeClass('d-none');
            $('#shop-details-in-single-page-of-product img').attr('src', selectedShopLogo);
        }
        else {
            $('#shop-details-in-single-page-of-product i').removeClass('d-none');
            $('#shop-details-in-single-page-of-product img').addClass('d-none');
        }

        // Change product guarantee
        // eq(equal) starts from 0
        var selectedGuarantee = selectedSeller.find('td:eq(1)').html();
        $('#product-guarantee-in-single-page-of-product').html(selectedGuarantee);

        // Hide other sellers box if it has just one item
        var otherSellersCount = $('.other-sellers-table[variant-value="' + selectedVariantValue + '"] tbody tr').length;
        if (otherSellersCount === 1) {
            $('#other-sellers-box, #other-sellers-count-box').addClass('d-none');
        } else {
            $('#other-sellers-box, #other-sellers-count-box').removeClass('d-none');
        }

        // Change other sellers count
        $('#other-sellers-count-box span').html((otherSellersCount - 1).toString().toPersinaDigit());

        // Change product score
        var selectedScore = selectedSeller.find('td:eq(3) span').html();
        $('#product-score-in-single-page-of-product span').html(selectedScore);

        // Show or hide free delivery box
        if (selectedSeller.attr('free-delivery') === 'true') {
            $('#free-delivery-box').removeClass('d-none');
        } else {
            $('#free-delivery-box').addClass('d-none');
        }

        // Show or hide "cart section"
        // کل کارت سکشن های بخش
        // Left side box
        // رو مخفی میکنیم
        $('#product-info-left-side-box .product-variant-in-cart-section').addClass('d-none');
        // کارت سکشن بخش سایر فروشندگان رو هم مخفی میکنیم
        $('.product-variant-in-cart-section[variant-id="' + selectedProductVariantId + '"]').addClass('d-none');
        var cartSectionEl = $('#product-info-left-side-box .product-variant-in-cart-section[variant-id="' + selectedProductVariantId + '"]');

        // و باید از تریم استفاده کنیم، اگر تعداد داخل سبد خرید صفر نبود، بخش کارت سکشن
        // Left side box
        // و بخش سایر فروشندگان رو نشون میدیم
        // چون که در داخل المنت اینتر زدیم به خاطر همین یکسری متن اضافه وجود داره و باید از
        // Trim
        // استفاده کنیم
        if (cartSectionEl.find('.product-variant-count-in-cart span:first').text().trim() !== '۰') {
            $('.product-variant-in-cart-section[variant-id="' + selectedProductVariantId + '"]').removeClass('d-none');
        }

        // Show or hide "add product to cart button"
        // کل دکمه های افزودن به سبد خرید در بخش
        // Left side box
        // رو مخفی میکنیم
        $('#product-info-left-side-box .add-product-variant-to-cart').addClass('d-none');
        // دکمه افزودن به سبد خرید در بخش سایر فروشندگان رو هم مخفی میکنیم
        $('.add-product-variant-to-cart[variant-id="' + selectedProductVariantId + '"]').addClass('d-none');

        // اگر تعداد داخل سبد خرید برای این تنوع انتخابی صفر بود
        // دکمه افزودن به سبد خرید رو در بخش
        // Left side box
        // و سایر فروشندگان نمایش می دهیم
        // چون که در داخل المنت اینتر زدیم به خاطر همین یکسری متن اضافه وجود داره و باید از
        // Trim
        // استفاده کنیم
        if (cartSectionEl.find('.product-variant-count-in-cart span:first').text().trim() === '۰') {
            $('.add-product-variant-to-cart[variant-id="' + selectedProductVariantId + '"]').removeClass('d-none');
        }
    }
});

// این فانکشن بعد از عملیات سمت سرور اجرا میشود
// True = Add from favorites table
// False = Remove from favorites table
function addFavoriteFunction() {
    var addFavoriteButton = $('#addFavoriteButton').parent().find('input[name="addFavorite"]');
    if (addFavoriteButton.val() === 'true') {
        // این محصول به علاقه مندی کاربر اضافه شده
        // پس باید آیکون قلب قرمز رو نشون بدیم
        // و مقدار اینپوت رو فالس کنیم که اگه دوباره روش کلیک کرد
        // اینبار عملیات حذف اون رکورد از بخش علاقه مندی انجام بدیم
        addFavoriteButton.val('false');
        $('#addFavoriteButton i:first').addClass('d-none');
        $('#addFavoriteButton i:last').removeClass('d-none');
    }
    else {
        // این محصول از علاقه مندی کاربر حذف شده
        // پس باید آیکون قلب قرمز رو مخفی کنیم
        // و مقدار اینپوت رو ترو کنیم که اگه دوباره روش کلیک کرد
        // اینبار عملیات افزودن اون رکورد به بخش علاقه مندی رو انجام بدیم
        addFavoriteButton.val('true');
        $('#addFavoriteButton i:first').removeClass('d-none');
        $('#addFavoriteButton i:last').addClass('d-none');
    }
}