$(function () {
    $(document).on('click', '.remove-product-variant-tr', function () {
        var currentVariantCode = $(this).parents('tr').attr('variant-code');
        $('#variant-code-items-form-in-create-consignment')
            .find('input[value="' + currentVariantCode + '"]').remove();
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

    $('#variant-code-items-form-in-create-consignment').submit(function () {
        if ($(this).valid()) {
            $(this).find('input:hidden').not('input[name="' + rvt + '"]').remove();
            $(this).find('table tbody tr').each(function () {
                var currentVariantCode = $(this).attr('variant-code');
                var currentProductCount = $(this).find('input').val();
                var parsedProductCount = parseInt(currentProductCount);
                if (parsedProductCount > maxCount || parsedProductCount < 1) {
                    showToastr('warning', `تعداد هر محصول باید بین 1 تا ${maxCount} باشد`);
                    return false;
                }
                $('#variant-code-items-form-in-create-consignment').prepend(
                    `<input name="CreateConsignment.Variants" type="hidden" value="${currentVariantCode}|||${currentProductCount}" />`
                );
            });
        }
    });
});

var maxCount = 100000;
function appendProductVariantTr(result) {
    $('#record-not-found-box').addClass('d-none');
    $('#consignment-items').append(result);
    $('#consignment-items tr:last').find('input').attr('max', maxCount);
    var currentVariantCode = $('#consignment-items tr:last').attr('variant-code');
    $('#variant-code-items-form-in-create-consignment').prepend(
        `<input type="hidden" value="${currentVariantCode}" />`
    );
    $('#send-consignment-submit-button').removeAttr('disabled');
}