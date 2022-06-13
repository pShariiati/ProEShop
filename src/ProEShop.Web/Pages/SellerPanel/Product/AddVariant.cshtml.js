$(function () {
    var variantTitle = $('#variant-box label span').html();
    $('.variant-item-button').click(function () {
        var selectedVariantId = $(this).attr('variant-id');
        $('#Variant_VariantId').val(selectedVariantId);
        $('.public-ajax-form').validate().element('#Variant_VariantId');
        var selectedButtonText = $(this).html();
        $('#variant-box label span').html(variantTitle + selectedButtonText);
    });
});

function addProductVariantFunction(message, data) {
    showToastr('success', message);

    // data = Url to go
    location.href = data;
}