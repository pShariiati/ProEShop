$(function () {
    fillDataTable();
    initializingAutocomplete();

    const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('delivery-date-icon-inventory-consignment'), {
        targetTextSelector: '#Consignments_SearchConsignments_DeliveryDate',
        persianNumber: true,
        selectedDate: new Date(),
        selectedDateToShow: new Date()
    });
});

function getConsignmentDetails(e) {
    var consignmentId = $(e).attr('consignment-id');
    getHtmlWithAJAX('?handler=GetConsignmentDetails', { consignmentId: consignmentId }, 'showConsignmentDetailsInModal', e);
}

function showConsignmentDetailsInModal(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currentModal = $('#html-modal-place');
    currentModal.find('.modal-body').html(result);
    currentModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
}

function confirmationConsignment(message) {
    showToastr('success', message);
    fillDataTable();
}

function changeConsignmentStatus(e) {
    var consignmentId = $(e).attr('consignment-id');
    getHtmlWithAJAX('?handler=ChangeConsignmentStatus', { consignmentId: consignmentId }, 'showChangeConsignmentStatusInModal', e);
}

function showChangeConsignmentStatusInModal(result, clickedButton) {
    appendHtmlModalPlaceToBody();
    var currentModal = $('#html-modal-place');
    currentModal.find('.modal-body').html(result);
    currentModal.modal('show');
    $('#html-modal-place .modal-header h5').html($(clickedButton).text().trim());
    initializeTinyMCE();
    $.validator.unobtrusive.parse($('#html-modal-place form'));
}

function consignmentReceivedAndAddStockSuccess(message) {
    fillDataTable();
    showToastr('success', message);
    $('#html-modal-place').modal('hide');
}