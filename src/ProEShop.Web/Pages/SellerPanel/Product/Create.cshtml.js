function getCategories() {
    getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, null, 'showCategories', null);
}
$(function () {
    getCategories();
});

var selectedCategoriesIds = [];

function showCategories(data) {
    $('#product-category div.row.card-body').html(data);
    $('#product-category div.row.card-body button[has-child=true]').click(function () {
        var selectedCategoryId = $(this).attr('category-id');
        selectedCategoriesIds.push(selectedCategoryId);
        getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, { selectedCategoriesIds: selectedCategoriesIds }, 'showCategories', null);
    });
}