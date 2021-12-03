$.get(`${location.pathname}?handler=GetDataTable`, function (data, stauts) {
    if (stauts == 'success') {
        $('.read-data-table').html(data);
    }
    else {
        showToastr('error', 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
    }
});