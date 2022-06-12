$(function () {
    $('.variant-item-button').click(function () {
        var selectedVariantId = $(this).attr('variant-id');
        $('#Variant_VariantId').val(selectedVariantId);
        $('.public-ajax-form').validate().element('#Variant_VariantId');
        var selectedButtonText = $(this).html();
        $('#variant-box label span').html('تنوع' + selectedButtonText);
    });
});