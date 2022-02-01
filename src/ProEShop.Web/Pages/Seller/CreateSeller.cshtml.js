$('#legal-person-checkbox-create-seller').change(function () {
    var labelEl = $(this).parents('.form-switch').find('label');
    if (this.checked) {
        addRequiredRule('#CreateSeller_CompanyName');
        addRequiredRule('#CreateSeller_RegisterNumber');
        addRequiredRule('#CreateSeller_EconomicCode');
        addRequiredRule('#CreateSeller_SignatureOwners');
        addRequiredRule('#CreateSeller_NationalId');
        addRangeRule('#CreateSeller_CompanyType');
        labelEl.html('شخص حقوقی');
    }
    else {
        removeRequiredRule('#CreateSeller_CompanyName');
        removeRequiredRule('#CreateSeller_RegisterNumber');
        removeRequiredRule('#CreateSeller_EconomicCode');
        removeRequiredRule('#CreateSeller_SignatureOwners');
        removeRequiredRule('#CreateSeller_NationalId');
        removeRangeRule('#CreateSeller_CompanyType');
        labelEl.html('شخص حقیقی');
    }
    $(this).parents('form').valid();
    $('#legal-person-box-create-seller').slideToggle();
});
$('#legal-person-box-create-seller').hide(0);

function removeRequiredRule(selector) {
    $(selector).rules('remove', 'required');
}

function removeRangeRule(selector) {
    $(selector).rules('remove', 'range');
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

function addRangeRule(selector) {
    var displayName = $(selector).parent().find('label').html().trim();
    $(selector).rules('add', {
        range: [0, 4],
        messages: {
            range: `لطفا ${displayName} را وارد نمایید`
        }
    });
}

var firstTab = $('#create-seller-container .nav-tabs button:first').attr('data-bs-target');
var lastTab = $('#create-seller-container .nav-tabs button:last').attr('data-bs-target');

$('#create-seller-container #previous-tab-create-seller').attr('disabled', 'disabled');
var currentTab = $('#create-seller-container .nav-tabs button:first').attr('data-bs-target');

$('#create-seller-container #next-tab-create-seller').click(function () {
    var nextTab = $(`#create-seller-container .nav-tabs button[data-bs-target="${currentTab}"]`).next();
    if (nextTab.attr('data-bs-target')) {
        currentTab = nextTab.attr('data-bs-target');
        nextTab.tab('show');
    }
});

$('#create-seller-container #previous-tab-create-seller').click(function () {
    var prevTab = $(`#create-seller-container .nav-tabs button[data-bs-target="${currentTab}"]`).prev();
    if (prevTab.attr('data-bs-target')) {
        currentTab = prevTab.attr('data-bs-target');
        prevTab.tab('show');
    }
});

$('#create-seller-container .nav-tabs button').on('show.bs.tab', function (e) {
    currentTab = $(e.target).attr('data-bs-target');
    if (currentTab == lastTab) {
        $('#create-seller-container #next-tab-create-seller').attr('disabled', 'disabled');
    }
    else {
        $('#create-seller-container #next-tab-create-seller').removeAttr('disabled');
    }
    if (currentTab == firstTab) {
        $('#create-seller-container #previous-tab-create-seller').attr('disabled', 'disabled');
    }
    else {
        $('#create-seller-container #previous-tab-create-seller').removeAttr('disabled');
    }
});

$('#CreateSeller_ProvinceId').change(function () {
    var formData = {
        provinceId: $(this).val()
    }
    getDataWithAJAX('/Seller/CreateSeller/test?handler=GetCities', formData, 'putCitiesInTheSelectBox');
});

function putCitiesInTheSelectBox(message, data) {
    $('#CreateSeller_CityId option').remove();
    $('#CreateSeller_CityId').append('<option value="0">انتخاب کنید</optoin>');
    $.each(data, function (key, value) {
        $('#CreateSeller_CityId').append(`<option value="${key}">${value}</optoin>`);
    });
}

const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('birth-date-icon-create-seller'), {
    targetTextSelector: '#CreateSeller_BirthDate',
    persianNumber: true,
    selectedDate: new Date($('#CreateSeller_BirthDate').attr('birth-date-en') || new Date()),
    selectedDateToShow: new Date($('#CreateSeller_BirthDate').attr('birth-date-en') || new Date())
});

$('#CreateSeller_CompanyName').val('اسم شرکت تست');
$('#CreateSeller_RegisterNumber').val('456465456465456');
$('#CreateSeller_EconomicCode').val('123123123123');
$('#CreateSeller_SignatureOwners').val('علی احمدی - محمد محمودی');
$('#CreateSeller_NationalId').val('56456465456456');
$('#CreateSeller_CompanyType').val('2');
$('#ShopName').val('فروشگاه تست');
$('#CreateSeller_Address').val('آدرس کامل');
$('#CreateSeller_Website').val('https://google.com');
$('#CreateSeller_PostalCode').val('1234567890');
$('#CreateSeller_ShabaNumber').val('12345678901234567890');
$('#CreateSeller_Telephone').val('02122334455');
$('#CreateSeller_AboutSeller').val('<h3>Hello</h3>');
var firstOptionProvince = $('#CreateSeller_ProvinceId option:eq(1)').val();
$('#CreateSeller_AcceptToTheTerms').attr('checked', true);
setTimeout(function () {
    $('#CreateSeller_ProvinceId').val(firstOptionProvince).trigger('change');
}, 500);