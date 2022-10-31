$(function () {
    fillDataTable();

    const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('created-date-time-in-orders'), {
        targetTextSelector: '#Orders_SearchOrders_CreatedDateTime',
        persianNumber: true,
        selectedDate: new Date(),
        selectedDateToShow: new Date()
    });
});