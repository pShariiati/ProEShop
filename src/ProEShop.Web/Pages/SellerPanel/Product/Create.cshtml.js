function getCategories() {
    getHtmlWithAJAX(`${location.pathname}?handler=GetCategories`, null, 'showCategories', null);
}
$(function () {
    getCategories();
});
function showCategories(data) {
    console.log(data);
}