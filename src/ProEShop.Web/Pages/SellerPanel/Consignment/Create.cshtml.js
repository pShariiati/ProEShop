$(function () {
    $(document).on('click', '.remove-product-variant-tr', function () {
        $(this).parents('tr').remove();
        if ($('#consignment-items tr').length === 0) {
            $('#record-not-found-box').removeClass('d-none');
            $('#send-consignment-submit-button').attr('disabled', 'disabled');
        }
    });

    const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('delivery-date-in-create-consignment'), {
        targetTextSelector: '#CreateConsignment_DeliveryDate',
        persianNumber: true
    });

    $('.get-html-by-sending-form').submit(function () {
        var selectedVariantCode = $('#VariantCode').val();
        if ($('#variant-code-items-form-in-create-consignment input[value="' + selectedVariantCode + '"]').length > 0) {
            showToastr('warning', 'این تنوع محصول از قبل اضافه شده است');
            return false;
        }
    });
});

function appendProductVariantTr(result) {
    $('#record-not-found-box').addClass('d-none');
    $('#consignment-items').append(result);
    var currentVariantCode = $('#consignment-items tr:last').attr('variant-code');
    $('#variant-code-items-form-in-create-consignment').prepend(
        `<input type="hidden" value="${currentVariantCode}" />`
    );
    $('#send-consignment-submit-button').removeAttr('disabled');
}