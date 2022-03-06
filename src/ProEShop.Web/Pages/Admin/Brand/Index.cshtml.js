fillDataTable();
appendFormModalPlaceToBody();
function actionsAfterLoadModalForm() {
    $('#IsIranianBrand').change(function () {
        var textToReplace = this.checked ? 'ایرانی' : 'خارجی';
        $(this).parents('.form-switch').find('label').html(textToReplace);
    });
}