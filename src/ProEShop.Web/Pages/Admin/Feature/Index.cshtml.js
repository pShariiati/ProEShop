$(function () {
    fillDataTable();

    $('#Features_SearchFeatures_CategoryId').change(function () {
        var selectedCategoryId = $(this).val();
        if (selectedCategoryId == 0) {
            $('#add-feature-for-selected-category').addClass('d-none');
        }
        else {
            var selectedCategoryText = $(this).find('option:selected').text();
            var addCategoryFeatureLink = $('#add-category-feature-link').attr('href');
            $('#add-feature-for-selected-category').removeClass('d-none');
            $('#add-feature-for-selected-category')
                .html(`افزودن ویژگی دسته بندی برای "${selectedCategoryText}"`);
            $('#add-feature-for-selected-category')
                .attr('href', `${addCategoryFeatureLink}&categoryId=${selectedCategoryId}`);
        }
    });
});

function customAjaxFormFunction() {
    $('#Title').val('');
    $('#Title').focus();
}