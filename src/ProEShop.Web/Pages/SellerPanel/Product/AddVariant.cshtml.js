$(function () {
    var variantTitle = $('#variant-box label span').html();
    $('.variant-item-button').click(function () {
        var selectedVariantId = $(this).attr('variant-id');
        $('#Variant_VariantId').val(selectedVariantId);
        $('.public-ajax-form').validate().element('#Variant_VariantId');
        var selectedButtonText = $(this).html();
        $('#variant-box label span').html(variantTitle + selectedButtonText);
    });

    $('.custom-select2').select2({
        theme: 'bootstrap-5',
        ajax: {
            url: location.pathname + '?handler=GetGuarantees',
            delay: 250,
            data: function (params) {
                return {
                    input: params.term
                };
            },
            cache: true
        },
        placeholder: 'انتخاب کنید',
        minimumInputLength: 2
    });
});

function addProductVariantFunction(message, data) {
    showToastr('success', message);

    // data = Url to go
    location.href = data;
}