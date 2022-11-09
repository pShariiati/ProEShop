$(function () {
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
    appendHtmlModalPlaceToBody();
    var currentModal = $('#html-modal-place');
    currentModal.find('.modal-body').html(result);
    currentModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
    convertEnglishNumbersToPersianNumber();
}