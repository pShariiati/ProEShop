$(function () {

    $(document).on('click', '.copy-post-tracking-code-button', function() {
        var postTrackingCode = $(this).attr('post-tracking-code');
        copyTextToClipboard(postTrackingCode, 'copyParcelPostLinkToClipboardFunction', $(this));
    });

    fillDataTable();

    const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('created-date-time-in-orders'), {
        targetTextSelector: '#Orders_SearchOrders_CreatedDateTime',
        persianNumber: true,
        selectedDate: new Date(),
        selectedDateToShow: new Date()
    });

    $('#Orders_SearchOrders_ProvinceId').change(function () {
        var formData = {
            provinceId: $(this).val()
        }

        if (formData.provinceId === '') {
            $('#Orders_SearchOrders_CityId option').remove();
            $('#Orders_SearchOrders_CityId').append('<option value="">لطفا ابتدا استان را انتخاب نمایید</option>');
            return;
        }
        getDataWithAJAX('?handler=GetCities', formData, 'putCitiesInTheSelectBox');
    });
});

function copyParcelPostLinkToClipboardFunction(clickedEl) {
    $(clickedEl).find('i').addClass('d-none');
    $(clickedEl).find('span:last').removeClass('d-none');

    // این فانکشن فقط یکبار فراخوانی میشود
    // و بهترین گزینه برای این سناریو می باشد
    setTimeout(function () {
        $(clickedEl).find('i').removeClass('d-none');
        $(clickedEl).find('span:last').addClass('d-none');
    }, 2000);
}

function putCitiesInTheSelectBox(message, data) {
    $('#Orders_SearchOrders_CityId option').remove();
    $('#Orders_SearchOrders_CityId').append('<option value="">انتخاب کنید</optoin>');
    $.each(data, function (key, value) {
        $('#Orders_SearchOrders_CityId').append(`<option value="${key}">${value}</optoin>`);
    });
}

function getOrderDetails(e) {
    var orderId = $(e).attr('order-id');
    getHtmlWithAJAX('?handler=GetOrderDetails', { orderId: orderId }, 'showOrderDetailsInModal', e);
}

function showOrderDetailsInModal(result, clickedButton) {
    appendHtmlScrollableModalPlaceToBody();
    var currentModal = $('#html-scrollable-modal-place');
    currentModal.find('.modal-body').html(result);
    currentModal.modal('show');
    $('#html-scrollable-modal-place .modal-header h5').html($(clickedButton).text().trim());
    convertEnglishNumbersToPersianNumber();
}


function changeStatusToDeliveryToPost(e) {
    var parcelPostId = $(e).attr('parcel-post-id');
    getHtmlWithAJAX('?handler=ShowDeliveryToPostPartial', { id: parcelPostId }, 'showChangeStatusToDeliveryToPost', e);
}

function showChangeStatusToDeliveryToPost(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currnetModal = $('#html-modal-place');
    currnetModal.find('.modal-body').html(result);
    currnetModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
    $.validator.unobtrusive.parse($('#html-modal-place form'));
}

// بعد از تحویل مرسوله به پست، گرید را رفرش میکنیم
function changeStatusToDeliveryToPostFunction(message, data) {
    showToastr('success', message);
    fillDataTable();
}