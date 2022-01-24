$('#legal-person-checkbox-create-seller').change(function () {
    var labelEl = $(this).parents('.form-switch').find('label');
    if (this.checked) {
        addRequiredRule('#CreateSeller_CompanyName');
        addRequiredRule('#CreateSeller_RegisterNumber');
        addRequiredRule('#CreateSeller_EconomicCode');
        addRequiredRule('#CreateSeller_SignatureOwners');
        addRequiredRule('#CreateSeller_NationalId');
        labelEl.html('شخص حقوقی');
    }
    else {
        removeRequiredRule('#CreateSeller_CompanyName')
        removeRequiredRule('#CreateSeller_RegisterNumber')
        removeRequiredRule('#CreateSeller_EconomicCode')
        removeRequiredRule('#CreateSeller_SignatureOwners')
        removeRequiredRule('#CreateSeller_NationalId')
        labelEl.html('شخص حقیقی');
    }
    $(this).parents('form').valid();
    $('#legal-person-box-create-seller').slideToggle();
});
$('#legal-person-box-create-seller').hide(0);

function removeRequiredRule(selector) {
    $(selector).rules('remove', 'required');
}

function addRequiredRule(selector) {
    var displayName = $(selector).parent().find('label').html().trim();
    $(selector).rules('add', {
        required: true,
        messages: {
            required: `لطفا ${displayName} را وارد نمایید`
        }
    });
}