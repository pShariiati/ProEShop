$(function () {

    $('.show-modal-form-button').click(function (e) {
        e.preventDefault();
        var urlToLoadTheForm = $(this).attr('href');
        showLoading();
        $.get(urlToLoadTheForm, function (data, status) {
            hideLoading();
            if (status == 'success') {
                $('#form-modal-place .modal-body').html(data);
                initializeTinyMCE();
                initializeSelect2();
                $.validator.unobtrusive.parse($('#form-modal-place form'));
                $('#form-modal-place').modal('show');
            }
            else {
                showErrorMessage();
            }
        });
    });

    function fillDataTable() {
        $.get(`${location.pathname}?handler=GetDataTable`, function (data, status) {
            $('.search-form-loading').removeAttr('disabled');
            $('.data-table-loading').addClass('d-none');
            if (status == 'success') {
                $('.data-table-place .data-table-body').remove();
                $('.data-table-place').append(data);
            }
            else {
                showErrorMessage();
            }
        });
    }

    fillDataTable();

    $(document).on('submit', 'form.custom-ajax-form', function (e) {
        e.preventDefault();
        var currentForm = $(this);
        var formAction = currentForm.attr('action');
        var formData = new FormData(this);
        $.ajax({
            url: formAction,
            data: formData,
            type: 'POST',
            enctype: 'multipart/form-data',
            dataType: 'json',
            processData: false,
            contentType: false,
            beforeSend: function () {
                currentForm.find('.submit-custom-ajax-button span').removeClass('d-none');
                currentForm.find('.submit-custom-ajax-button').attr('disabled', 'disabled');
            },
            success: function (data, status) {
                if (data.isSuccessful == false) {
                    fillValidationForm(data.data, currentForm);
                    showToastr('warning', data.message);
                }
                else {
                    fillDataTable();
                    $('#form-modal-place').modal('hide');
                    showToastr('success', data.message);
                }
            },
            complete: function () {
                currentForm.find('.submit-custom-ajax-button span').addClass('d-none');
                currentForm.find('.submit-custom-ajax-button').removeAttr('disabled');
            },
            error: function () {
                showErrorMessage();
            }
        });
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
                    fillValidationForm(data.data, currentForm);
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
    function fillValidationForm(errors, currentForm) {
        var result = '<ul>';
        errors.forEach(function (e) {
            result += `<li>${e}</li>`;
        });
        result += '</ul>';
        currentForm.find('div[class*="validation-summary"]').html(result);
    }
});