fillDataTable();

function getSellerDetails(e) {
    debugger;
    var sellerId = $(e).attr('seller-id');
    getHtmlWithAJAX('?handler=GetSellerDetails', { sellerId: sellerId }, 'showSellerDetailsInModal')
}

function showSellerDetailsInModal(result) {
    debugger;
    console.log(result);
}