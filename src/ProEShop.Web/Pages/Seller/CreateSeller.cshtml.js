$('#legal-person-checkbox-create-seller').change(function () {
    var labelEl = $(this).parents('.form-switch').find('label');
    if (this.checked) {
        labelEl.html('شخص حقوقی');
    }
    else {
        labelEl.html('شخص حقیقی');
    }
    $('#legal-person-box-create-seller').slideToggle();
});
$('#legal-person-box-create-seller').hide(0);