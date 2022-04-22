﻿function getCategories() {
    getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, null, 'showCategories', null);
}
$(function () {
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

$('#select-product-category-button').click(function () {
    var selectedCategoryId = $('#product-category div.list-group.col-4:last button.active').attr('category-id');
    getDataWithAJAX('?handler=GetCategoryBrands', { categoryId: selectedCategoryId }, 'showCategoryBrands');
    getDataWithAJAX('?handler=CanAddFakeProduct', { categoryId: selectedCategoryId }, 'changeIsFakeStatus');
    getHtmlWithAJAX('?handler=ShowCategoryFeatures', { categoryId: selectedCategoryId }, 'showCategoryFeatures');
    $('#request-new-brand-url').attr('href', requestNewBrandUrl + '&categoryId=' + selectedCategoryId);
});

function showCategoryFeatures(data) {
    $('#product-features .card-body.row').html(data);
}

function showCategoryBrands(message, data) {
    $('#Product_BrandId option').remove();
    $('#Product_BrandId').append('<option value="0">انتخاب کنید</option>');
    for (brandId in data) {
        $('#Product_BrandId').append(`<option value="${brandId}">${data[brandId]}</option>`);
    }
    $('#add-product-tab button[data-bs-target="#product-info"]').tab('show');
}

function changeIsFakeStatus(message, data) {
    if (data === false) {
        $('#Product_IsFake').attr('disabled', 'disabled');
        $('#Product_IsFake').prop('checked', false);
    }
    else {
        $('#Product_IsFake').removeAttr('disabled');
    }
}

$(document).on('change', '#IsIranianBrand', function () {
    var textToReplace = this.checked ? 'ایرانی' : 'خارجی';
    $(this).parents('.form-switch').find('label').html(textToReplace);
});

function createProductFunction(message, data) {
    alert(message);
}