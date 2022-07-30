$(function () {

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
        $('#product-variants-box-in-show-product-info div').removeClass('selected-variant-in-show-product-info');
        $('#product-variants-box-in-show-product-info i').addClass('d-none');

        $(this).find('i').removeClass('d-none');
        $(this).addClass('selected-variant-in-show-product-info');

        var selectedVariantValue = $(this).attr('data-bs-original-title');

        $('.other-sellers-table').addClass('d-none');

        $('.other-sellers-table[variant-value="' + selectedVariantValue + '"]').removeClass('d-none');

        // Change variant value
        $('#product-variant-value').html(selectedVariantValue);

        // Change product info in left side box
        var selectedSeller = $('.other-sellers-table[variant-value="' + selectedVariantValue + '"] tbody tr:first');

        // Change shop name
        var selectedShopName = selectedSeller.find('td:first').text();
        $('#shop-details-in-single-page-of-product div').html(selectedShopName);

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

        // Change product price
        var selectedPrice = selectedSeller.find('td:eq(2)').html();
        $('#product-price-in-single-page-of-product').html(selectedPrice);

        // Hide other sellers box if it has just one item
        var otherSellersCount = $('.other-sellers-table[variant-value="' + selectedVariantValue + '"] tbody tr').length;
        if (otherSellersCount === 1) {
            $('#other-sellers-box, #other-sellers-count-box').addClass('d-none');
        } else {
            $('#other-sellers-box, #other-sellers-count-box').removeClass('d-none');
        }

        // Change other sellers count
        $('#other-sellers-count-box span').html(otherSellersCount - 1);

        // Change product score
        var selectedPrice = selectedSeller.find('td:eq(3) span').html();
        $('#product-score-in-single-page-of-product span').html(selectedPrice);

        // Show or hide free delivery box
        if (selectedSeller.attr('free-delivery') === 'true') {
            $('#free-delivery-box').removeClass('d-none');
        } else {
            $('#free-delivery-box').addClass('d-none');
        }
    });

    // Change variants (size)
    $('#product-variants-box-in-show-product-info select').change(function () {

        var selectedVariantValue = this.value;

        $('.other-sellers-table').addClass('d-none');

        $('.other-sellers-table[variant-value="' + selectedVariantValue + '"]').removeClass('d-none');

        // Change variant value
        $('#product-variant-value').html(selectedVariantValue);

        // Change product info in left side box
        var selectedSeller = $('.other-sellers-table[variant-value="' + selectedVariantValue + '"] tbody tr:first');

        // Change shop name
        var selectedShopName = selectedSeller.find('td:first').text();
        $('#shop-details-in-single-page-of-product div').html(selectedShopName);

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

        // Change product price
        var selectedPrice = selectedSeller.find('td:eq(2)').html();
        $('#product-price-in-single-page-of-product').html(selectedPrice);

        // Hide other sellers box if it has just one item
        var otherSellersCount = $('.other-sellers-table[variant-value="' + selectedVariantValue + '"] tbody tr').length;
        if (otherSellersCount === 1) {
            $('#other-sellers-box, #other-sellers-count-box').addClass('d-none');
        } else {
            $('#other-sellers-box, #other-sellers-count-box').removeClass('d-none');
        }

        // Change other sellers count
        $('#other-sellers-count-box span').html(otherSellersCount - 1);

        // Change product score
        var selectedPrice = selectedSeller.find('td:eq(3) span').html();
        $('#product-score-in-single-page-of-product span').html(selectedPrice);

        // Show or hide free delivery box
        if (selectedSeller.attr('free-delivery') === 'true') {
            $('#free-delivery-box').removeClass('d-none');
        } else {
            $('#free-delivery-box').addClass('d-none');
        }
    });
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