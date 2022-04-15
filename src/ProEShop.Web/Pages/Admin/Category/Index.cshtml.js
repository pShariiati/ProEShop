$(function () {
    fillDataTable();
});

var brandBox = `<div class="btn-group m-1">
                <button type="button" class="btn btn-outline-dark">
                    [brand title]
                </button>
                <button type="button" class="btn btn-danger remove-selected-brand">
                    <i class="bi bi-x-lg"></i>
                </button>
            </div>`

function onAutocompleteSelect(event, ui) {
    var enteredBrand = ui.item.value;
    event.preventDefault();
    $(event.target).val('');
    if ($('#add-brand-to-category-form input[type="hidden"][value="' + enteredBrand + '"]').length == 0) {
        var brandBoxToAppend = brandBox.replace('[brand title]', enteredBrand);
        $('#empty-selected-brands').addClass('d-none');
        $('#selected-brands-box').append(brandBoxToAppend);
        var inputToAppend = `<input type="hidden" name="SelectedBrands" value="${enteredBrand}" />`;
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
    $('#add-brand-to-category-form input[value="' + selectedBrandText + '"]').remove();
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