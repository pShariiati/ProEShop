$(function () {

    function activatingDeleteButtons() {
        $('.delete-row-button').click(function () {
            var currentForm = $(this).parent();
            Swal.fire({
                title: 'اعلان',
                text: 'آیا مطمئن به حذف هستید ؟',
                icon: 'warning',
                confirmButtonText: 'بله',
                showDenyButton: true,
                denyButtonText: 'خیر',
                confirmButtonColor: '#067719',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    var data = {
                        elementId: currentForm.find('input:first').val(),
                        __RequestVerificationToken: currentForm.find('input:last').val()
                    }
                    showLoading();
                    $.post(location.pathname + "?handler=Delete", data, function (data, status) {
                        if (data.isSuccessful == false) {
                            showToastr('warning', data.message);
                        }
                        else {
                            fillDataTable();
                            showToastr('success', data.message);
                        }
                    }).always(function () {
                        hideLoading();
                    }).fail(function () {
                        showErrorMessage();
                    });
                }
            });
        });
    }

    function activatingModalForm() {
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
    }
    activatingModalForm();

    function activatingPagination() {
        $('#main-pagianation button').click(function () {
            isMainPaginationClicked = true;
            var currentPageSelected = $(this).val();
            $('.search-form-via-ajax input[name$="Pagination.CurrentPage"]').val(currentPageSelected);
            $('.search-form-via-ajax').submit();
        });
    }

    function activatingGotoPage() {
        $('#go-to-page-button').click(function () {
            isGotoPageClicked = true;
        });
    }

    function fillDataTable() {
        $.get(`${location.pathname}?handler=GetDataTable`, function (data, status) {
            $('.search-form-loading').removeAttr('disabled');
            $('.data-table-loading').addClass('d-none');
            if (status == 'success') {
                $('.data-table-place .data-table-body').remove();
                $('.data-table-place').append(data);
                activatingPagination();
                activatingGotoPage();
                activatingModalForm();
                activatingDeleteButtons();
                enablingTooltips();
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

    var isMainPaginationClicked = false;
    var isGotoPageClicked = false;

    $(document).on('submit', 'form.search-form-via-ajax', function (e) {
        e.preventDefault();
        var currentForm = $(this);
        var pageNumberInput = $('#page-number-input').val();
        if (isGotoPageClicked || $('#page-number-input').is(':focus')) {
            currentForm.find('input[name$="Pagination.CurrentPage"').val(pageNumberInput);
        }
        else if (!isMainPaginationClicked) {
            currentForm.find('input[name$="Pagination.CurrentPage"').val(1);
        }
        const formData = currentForm.serializeArray();
        // show loading and disabling button
        currentForm.find('.search-form-loading').attr('disabled', 'disabled');
        currentForm.find('.search-form-loading span').removeClass('d-none');

        $('.data-table-loading').removeClass('d-none');
        $('.data-table-body').html('');

        $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data, status) {
            isMainPaginationClicked = false;
            isGotoPageClicked = false;
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
                    activatingPagination();
                    activatingGotoPage();
                    activatingModalForm();
                    activatingDeleteButtons();
                    enablingTooltips();
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