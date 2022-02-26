function getCategories() {
    getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, null, 'showCategories', null);
}
$(function () {
    getCategories();
});

var selectedCategoriesIds = [];

function showCategories(data) {
    $('#product-category div.row.card-body').html(data);
    selectedCategoriesIds.forEach(element => {
        $('#product-category button[category-id=' + element + ']').addClass('active');
    });
    $('#product-category div.row.card-body button[has-child=true]').click(function () {
        var selectedRow = parseInt($(this).parent().attr('category-row'));
        debugger;
        for (var counter = selectedRow; counter <= selectedCategoriesIds.length; counter++) {
            debugger;
            $('#product-category div[category-row=' + counter + '] button').removeClass('active');
        }
        $(this).addClass('active');
        selectedCategoriesIds = [];
        $('#product-category button.active').each(function () {
            debugger;
            var a = $(this).attr('category-id');
            console.log(a);
            selectedCategoriesIds.push(a);
        });
        getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, { selectedCategoriesIds: selectedCategoriesIds }, 'showCategories', null);
    });
}