$(function () {

    // زمانی که روی دکمه حذف کردن تنوع دسته بندی کلیک شد
    // استفاده شده در مودال ویرایش تنوع دسته بندی
    $(document).on('click', '.remove-selected-variant-button', function() {
        var variantId = $(this).parent().attr('variant-id');

        // حذف اینپوت با تایپ
        // Hidden
        $(this).parents('form').find('input[name="SelectedVariants"][value="' + variantId + '"]').remove();

        // حذف خود دکمه از بخبش تنوع های این دسته بندی
        $(this).parent().remove();
    });

    // زمانیکه روی هر کدام از تنوع های بخش تمامی تنوع ها کلیک شد
    // باید اون تنوع رو به بخش تنوع های این دسته بندی اضافه کنیم
    $(document).on('click', '.variant-item-button', function() {
        var variantId = $(this).attr('variant-id');

        // اگه از قبل این تنوع اضافه شده بود
        if ($(this).parents('form').find('input[name="SelectedVariants"][value="' + variantId + '"]').length > 0) {
            showToastr('warning', 'این تنوع از قبل برای این دسته بندی اضافه شده است');
            return;
        }

        // اینپوت مخفی رو به فرم اضافه میکنیم که بعدا بتونیم اینو به سمت سرور بفرستیم
        $(this).parents('form').prepend('<input type="hidden" name="SelectedVariants" value="' + variantId + '" />');
        var variantToAppend =
            '<button variant-id="'+variantId+'" type="button" class="mx-1 p-2 badge rounded-pill bg-primary border-0">' +
                $(this).html() +
            '</button>';

        // باتن مربوطه رو به بخش تنوع های این دسته بندی اضافه میکنیم
        $('#selected-variants-box').append(variantToAppend);
        $('#selected-variants-box span:last').addClass('me-1');
        // اضافه کردن دکمه حذف کردن به تنوعی که الان اضافه کردیم
        $('#selected-variants-box button:last').append('<i class="bi bi-x-circle remove-selected-variant-button"></i>');
    });

    fillDataTable();
});

var brandBox = `<div class="btn-group m-1">
                <button type="button" class="btn btn-outline-dark">
                    [brand title]
                </button>
                <button type="button" class="btn btn-info text-white">
                    %
                    [commission percentage]
                </button>
                <button type="button" class="btn btn-danger remove-selected-brand">
                    <i class="bi bi-x-lg"></i>
                </button>
            </div>`

function onAutocompleteSelect(event, ui) {
    var enteredBrand = ui.item.value;
    event.preventDefault();
    $(event.target).val('');
    var commissionPercentage = $('#commission-percentage-input').val();
    if (isNullOrWhitespace(commissionPercentage)) {
        showToastr('error', 'لطفا درصد کمیسیون را وارد نمایید');
        return;
    }
    var parsedCommissionPercentage = parseInt(commissionPercentage);
    if (parsedCommissionPercentage > 20 || parsedCommissionPercentage < 1) {
        showToastr('error', 'درصد کمیسیون باید بین 1 تا 20 درصد باشد');
        return;
    }
    if ($('#add-brand-to-category-form input[type="hidden"][value^="' + enteredBrand + '|||"]').length == 0) {
        var brandBoxToAppend = brandBox.replace('[brand title]', enteredBrand);
        brandBoxToAppend = brandBoxToAppend.replace('[commission percentage]', commissionPercentage);
        $('#empty-selected-brands').addClass('d-none');
        $('#selected-brands-box').append(brandBoxToAppend);
        var inputToAppend = `<input type="hidden" name="SelectedBrands" value="${enteredBrand}|||${commissionPercentage}" />`;
        $('#add-brand-to-category-form').prepend(inputToAppend);
        showToastr('success', 'برند مورد نظر با موفقیت اضافه شد')
    }
    else {
        showToastr('warning', 'این برند از قبل اضافه شده است')
    }
}

$(document).on('click', '.remove-selected-brand', function () {
    var selectedBrandText = $(this).parent().find('button:first').text().trim();
    $(this).parent().remove();
    $('#add-brand-to-category-form input[value^="' + selectedBrandText + '|||"]').remove();
    showToastr('success', 'برند مورد نظر با موفقیت از این دسته بندی حذف شد')
    if ($('#selected-brands-box .btn-group').length == 0) {
        $('#empty-selected-brands').removeClass('d-none');
    }
});

$(document).on('keydown', '#search-brand', function () {
    if (event.key == 'Enter') {
        event.preventDefault();
    }
});