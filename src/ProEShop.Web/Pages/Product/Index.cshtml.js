$(function() {
    $('#show-all-product-features').click(function() {
        $(this).addClass('d-none');
        $('#features-next-to-product-box li').removeClass('d-none');
    });

    // Add border to the first variant
    $('#product-variants-box-in-show-product-info div:first').addClass('selected-variant-in-show-product-info');

    // Add check to the first variant
    $('#product-variants-box-in-show-product-info i:first').removeClass('d-none');
});