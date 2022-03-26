fillDataTable();
function actionsAfterLoadModalForm() {
    var isIranianBrandChecked = $('#IsIranianBrand').is(':checked');
    $('#IsIranianBrand').parents('.form-switch').find('label').html(isIranianBrandChecked ? 'ایرانی' : 'خارجی');
    $('#IsIranianBrand').change(function () {
        var textToReplace = this.checked ? 'ایرانی' : 'خارجی';
        $(this).parents('.form-switch').find('label').html(textToReplace);
    });
}