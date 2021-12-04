$(function () {
    $.get(`${location.pathname}?handler=GetDataTable`, function (data, status) {
        if (status == 'success') {
            $('.data-table-place').html(data);
        }
        else {
            showErrorMessage();
        }
    });

    $(document).on('submit', 'form.search-form-via-ajax', function (e) {
        e.preventDefault();
        var currentForm = $(this);
        const formData = currentForm.serializeArray();
        $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data, status) {
            if (status == 'success1') {
                if (data.isSuccessful == false) {
                    var errors = '<ul>';
                    data.data.forEach(function (e) {
                        errors += `<li>${e}</li>`;
                    });
                    errors += '</ul>';
                    currentForm.find('div[class*="validation-summary"]').html(errors);
                    showToastr('warning', data.message);
                }
                else {
                    $('.data-table-place').html(data);
                }
            }
            else {
                showErrorMessage();
            }
        });
    });
});