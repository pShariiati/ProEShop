function getCategories() {
    getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, null, 'showCategories', null);
}
$(function () {
    // Disable all tabs except first
    $('#add-product-tab button:not(:first)').attr('disabled', 'disabled');
    $('#add-product-tab button:not(:first)').addClass('not-allowed-cursor');

    $(document).on('click', '.go-to-next-tab', function () {
        var nextTabId = $(this).parents('.tab-pane').next().attr('id');
        $(`#add-product-tab button[data-bs-target="#${nextTabId}"]`).tab('show');
    });

    getCategories();
    activatingModalForm();

    var specialtyCheckTinyMce = tinymce.get('Product_SpecialtyCheck');
    specialtyCheckTinyMce.settings.max_height = 1000;
});

var selectedCategoriesIds = [];

function showCategories(data) {
    $('#product-category div.row.card-body').html(data);
    $('#selected-categories-for-add-product').html('');
    selectedCategoriesIds.forEach(element => {
        var currentCategory = $('#product-category button[category-id=' + element + ']');
        currentCategory.addClass('active');
        var currentCategoryText = currentCategory.text().trim();
        $('#selected-categories-for-add-product').append(
            `<span> ${currentCategoryText} <i class="bi bi-chevron-left"></i></span>`
        );
    });
    $('#product-category div.row.card-body button[has-child=true]').click(function () {

        $('#select-product-category-button').attr('disabled', 'disabled');
        $('#select-product-category-button').addClass('btn-light');
        $('#select-product-category-button').removeClass('btn-primary');

        var selectedRow = parseInt($(this).parent().attr('category-row'));
        for (var counter = selectedRow; counter <= selectedCategoriesIds.length; counter++) {
            $('#product-category div[category-row=' + counter + '] button').removeClass('active');
        }
        $('#product-category button[has-child=false]').removeClass('active');
        $(this).addClass('active');
        selectedCategoriesIds = [];
        $('#product-category button.active').each(function () {
            selectedCategoriesIds.push($(this).attr('category-id'));
        });
        getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, { selectedCategoriesIds: selectedCategoriesIds }, 'showCategories', null);
    });

    $('#product-category div.row.card-body button[has-child=false]').click(function () {
        var selectedRow = parseInt($(this).parent().attr('category-row'));
        $('#product-category div[category-row=' + selectedRow + '] button').removeClass('active');
        for (var counter = selectedRow; counter <= selectedCategoriesIds.length; counter++) {
            $('#product-category div[category-row=' + (selectedRow + 1) + ']').remove();
        }
        $(this).addClass('active');
        $('#selected-categories-for-add-product').html('');
        $('#product-category button.active').each(function () {
            var currentCategory = $(this);
            var currentCategoryText = currentCategory.text().trim();
            if (currentCategory.attr('has-child') === 'true') {
                $('#selected-categories-for-add-product').append(
                    `<span> ${currentCategoryText} <i class="bi bi-chevron-left"></i></span>`
                );
            }
            else {
                $('#selected-categories-for-add-product').append(
                    `<span> ${currentCategoryText}</span>`
                );
            }
        });

        $('#select-product-category-button').removeAttr('disabled');
        $('#select-product-category-button').removeClass('btn-light');
        $('#select-product-category-button').addClass('btn-primary');
    });

    $('#reset-product-category-button').click(function () {
        if ($('#selected-categories-for-add-product span').length > 0) {
            selectedCategoriesIds = [];
            getCategories();
        }
    });
}

var requestNewBrandUrl = $('#request-new-brand-url').attr('href');


var isCategoryAlreadySelected = false;
var selectedCategoryId;
$('#select-product-category-button').click(function () {
    if (isCategoryAlreadySelected) {
        showSweetAlert2('تغییر دسته بندی منجر به از بین رفتن تمامی اطلاعات وارد شده شما میشود، آیا مطمئن به انجام این کار هستید ؟', 'emptyAllInputsAndShowOtherTabs', 'undoSelectedCategoryButton')
    }
    else {
        // first time that page loaded
        emptyAllInputsAndShowOtherTabs();
    }
    isCategoryAlreadySelected = true;
});

function emptyAllInputsAndShowOtherTabs() {
    selectedCategoryId = $('#product-category div.list-group.col-4:last button.active').attr('category-id');
    $('#Product_CategoryId').val(selectedCategoryId);
    getDataWithAJAX('?handler=GetCategoryInfo', { categoryId: selectedCategoryId }, 'categoryInfo');
    $('#request-new-brand-url').attr('href', requestNewBrandUrl + '&categoryId=' + selectedCategoryId);
}

function undoSelectedCategoryButton() {
    $(`#product-category button`).removeClass('active');
    $(`#product-category button[category-id="${selectedCategoryId}"]`).addClass('active');
}

function categoryInfo(message, data) {
    // showCategoryBrands
    $('#Product_BrandId option').remove();
    $('#Product_BrandId').append('<option value="0">انتخاب کنید</option>');
    for (brandId in data.brands) {
        $('#Product_BrandId').append(`<option value="${brandId}">${data.brands[brandId]}</option>`);
    }
    $('#add-product-tab button[data-bs-target="#product-info"]').tab('show');

    // End showCategoryBrands

    // changeIsFakeStatus

    if (data.canAddFakeProduct === false) {
        $('#Product_IsFake').attr('disabled', 'disabled');
        $('#Product_IsFake').prop('checked', false);
    }
    else {
        $('#Product_IsFake').removeAttr('disabled');
    }

    // End changeIsFakeStatus

    // showCategoryFeatures

    $('#product-features .card-body.row').html(data.categoryFeatures);

    // End showCategoryFeatures

    initializeSelect2WithoutModal();

    // Active all tabs and remove not-allowed-cursor
    $('#add-product-tab button:not(:first)').removeAttr('disabled');
    $('#add-product-tab button:not(:first)').removeClass('not-allowed-cursor');

    // Empty all inputs
    $('#create-product-form input').not(`[name="${rvt}"], #Product_CategoryId`).val('');
    tinyMCE.get('Product_ShortDescription').setContent('');
    tinyMCE.get('Product_SpecialtyCheck').setContent('');
    $('#product-images-preview-box').html('');

    // Test data
    //$('#Product_PackWeight').val('1');
    //$('#Product_PackLength').val('1');
    //$('#Product_PackWidth').val('1');
    //$('#Product_PackHeight').val('1');
    //$('#Product_PersianTitle').val('Product_PersianTitle');
    //$('#Product_EnglishTitle').val('Product_EnglishTitle');

    //tinyMCE.get('Product_ShortDescription').setContent('Product_ShortDescription');
    //tinyMCE.get('Product_SpecialtyCheck').setContent('Product_SpecialtyCheck');

    //var a = $('#Product_BrandId option:last').val();
    //$('#Product_BrandId').select2('val', a);
}

$(document).on('change', '#IsIranianBrand', function () {
    var textToReplace = this.checked ? 'ایرانی' : 'خارجی';
    $(this).parents('.form-switch').find('label').html(textToReplace);
});

function createProductFunction(message, data) {
    showToastr('success', message);
    location.href = data;
}