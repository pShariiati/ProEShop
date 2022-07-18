$(function() {
    $('#show-all-product-features').click(function() {
        $(this).addClass('d-none');
        $('#features-next-to-product-box li').removeClass('d-none');
    });
});