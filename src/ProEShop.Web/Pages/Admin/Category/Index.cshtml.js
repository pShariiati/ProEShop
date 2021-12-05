$(function () {

    $('.show-modal-form-button').click(function (e) {
        e.preventDefault();
        var urlToLoadTheForm = $(this).attr('href');
        $.get(urlToLoadTheForm, function (data, status) {
            if (status == 'success') {
                $('#show-form-modal .modal-body').html(data);
                $.validator.unobtrusive.parse($('#show-form-modal form'));
                $('#show-form-modal').modal('show');
            }
            else {
                showErrorMessage();
            }
        });
    });

    $.get(`${location.pathname}?handler=GetDataTable`, function (data, status) {
        $('.search-form-loading').removeAttr('disabled');
        $('.data-table-loading').addClass('d-none');
        if (status == 'success') {
            $('.data-table-place').append(data);
        }
        else {
            showErrorMessage();
        }
    });

    $(document).on('submit', 'form.search-form-via-ajax', function (e) {
        e.preventDefault();
        var currentForm = $(this);
        const formData = currentForm.serializeArray();

        // show loading and disabling button
        currentForm.find('.search-form-loading').attr('disabled', 'disabled');
        currentForm.find('.search-form-loading span').removeClass('d-none');

        $('.data-table-loading').removeClass('d-none');
        $('.data-table-body').html('');

        $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data, status) {
            // hide loading and activating button
            currentForm.find('.search-form-loading').removeAttr('disabled');
            currentForm.find('.search-form-loading span').addClass('d-none');

            $('.data-table-loading').addClass('d-none');

            if (status == 'success') {
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
                    $('.data-table-place .data-table-body').html(data);
                }
            }
            else {
                showErrorMessage();
            }
        });
    });
});