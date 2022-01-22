//__RequestVerificationToken
var rvt = '__RequestVerificationToken';

var loadingModalHtml = `<div class="modal" id="loading-modal" data-bs-backdrop="static">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">لطفا صبر کنید</h5>
            </div>
            <div class="modal-body text-center">
                <img src="/images/application/loading.gif" />
            </div>
        </div>
    </div>
</div>`;
function showLoading() {
    if ($('#loading-modal').length === 0) {
        $('body').append(loadingModalHtml);
    }
    $('#loading-modal').modal('show');
}
function hideLoading() {
    $('#loading-modal').modal('hide');
}

// Toastr

function showToastr(status, message) {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr[status](message);
}

// End toastr

// Enabling tooltips
function enablingTooltips() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

enablingTooltips();

function showErrorMessage(message) {
    showToastr('error', message != null ? message : 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
}

function initializeTinyMCE() {
    if ($('textarea.custom-tinymce').length > 0) {
        tinymce.remove('textarea.custom-tinymce');
        tinymce.init({
            selector: 'textarea.custom-tinymce',
            height: 300,
            max_height: 500,
            language: 'fa_IR',
            language_url: '/js/fa_IR.js',
            content_style: 'body {font-family: Vazir}',
            plugins: 'link table preview wordcount',
            toolbar: [
                {
                    name: 'history', items: ['undo', 'redo', 'preview']
                },
                {
                    name: 'styles', items: ['styleselect']
                },
                {
                    name: 'formatting', items: ['bold', 'italic', 'underline', 'link']
                },
                {
                    name: 'alignment', items: ['alignleft', 'aligncenter', 'alignright', 'alignjustify', 'forecolor', 'backcolor']
                },
                {
                    name: 'table', items: ['table', 'wordcount']
                },
                {
                    name: 'indentation', items: ['outdent', 'indent']
                }
            ],
            menubar: false,
            branding: false
        });
    }
}

initializeTinyMCE();

document.addEventListener('focusin', function (e) {
    if (e.target.closest('.tox-tinymce-aux, .moxman-window, .tam-assetmanager-root') !== null) {
        e.stopImmediatePropagation();
    }
});

function initializeSelect2() {
    $('.custom-select2').select2({
        theme: 'bootstrap-5',
        dropdownParent: $('#form-modal-place')
    });
}

// Validation

// fileRequired

if (jQuery.validator) {
    jQuery.validator.addMethod("fileRequired", function (value, element, param) {
        if (element.files[0] != null)
            return element.files[0].size > 0;
        return false;
    });
    jQuery.validator.unobtrusive.adapters.addBool("fileRequired");

    // allowExtensions
    jQuery.validator.addMethod('allowExtensions', function (value, element, param) {
        if (element.files[0] != null) {
            var whiteListExtensions = $(element).data('val-whitelistextensions').split(',');
            return whiteListExtensions.includes(element.files[0].type);
        }
        return true;
    });
    jQuery.validator.unobtrusive.adapters.addBool('allowExtensions');

    // isImage
    jQuery.validator.addMethod('isImage', function (value, element, param) {
        if (element.files[0] != null) {
            var whiteListExtensions = $(element).data('val-whitelistextensions').split(',');
            return whiteListExtensions.includes(element.files[0].type);
        }
        return true;
    });
    jQuery.validator.unobtrusive.adapters.addBool('isImage');

    // maxFileSize
    jQuery.validator.addMethod('maxFileSize', function (value, element, param) {
        if (element.files[0] != null) {
            var maxFileSize = $(element).data('val-maxsize');
            var selectedFileSize = element.files[0].size;
            return maxFileSize >= selectedFileSize;
        }
        return true;
    });
    jQuery.validator.unobtrusive.adapters.addBool('maxFileSize');
}

// End validation

// Ajax operations

function activatingDeleteButtons() {
    $('.delete-row-button').click(function () {
        var currentForm = $(this).parent();
        var customMessage = $(this).attr('custom-message');
        var formData = currentForm.serializeArray();
        Swal.fire({
            title: 'اعلان',
            text: customMessage == undefined ? 'آیا مطمئن به حذف هستید ؟' : customMessage,
            icon: 'warning',
            confirmButtonText: 'بله',
            showDenyButton: true,
            denyButtonText: 'خیر',
            confirmButtonColor: '#067719',
            allowOutsideClick: false
        }).then((result) => {
            if (result.isConfirmed) {
                showLoading();
                $.post(currentForm.attr('action'), formData, function (data, status) {
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

function initializingAutocomplete() {
    if ($('.autocomplete').length > 0) {
        $('.autocomplete').autocomplete({
            source: `${location.pathname}?handler=AutocompleteSearch`,
            minLength: 2,
            delay: 500
        });
    }
}

function activatingModalForm() {

    $('.show-modal-form-button').click(function (e) {
        e.preventDefault();
        var urlToLoadTheForm = $(this).attr('href');
        var customTitle = $(this).attr('custom-title');
        if (customTitle == undefined) {
            customTitle = $(this).text().trim();
        }
        $('#form-modal-place .modal-header h5').html(customTitle);
        showLoading();
        $.get(urlToLoadTheForm, function (data, status) {
            if (data.isSuccessful == false) {
                showToastr('warning', data.message);
            }
            else {
                $('#form-modal-place .modal-body').html(data);
                initializeTinyMCE();
                initializeSelect2();
                initializingAutocomplete();
                $.validator.unobtrusive.parse($('#form-modal-place form'));
                $('#form-modal-place').modal('show');
            }
        }).fail(function () {
            showErrorMessage();
        }).always(function () {
            hideLoading();
        });
    });
}
//activatingModalForm();

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
    $('.data-table-place .data-table-body').remove();
    $('.search-form-submit-button').attr('disabled', 'disabled');
    $('.data-table-loading').removeClass('d-none');
    $('#record-not-found-box').remove();

    const formData = $('form.search-form-via-ajax').serializeArray();

    $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data, status) {
        $('.search-form-submit-button').removeAttr('disabled');
        $('.data-table-loading').addClass('d-none');
        if (status == 'success') {
            $('.data-table-place').append(data);
            activatingPagination();
            activatingGotoPage();
            activatingModalForm();
            activatingDeleteButtons();
            activatingPageCount();
            enablingTooltips();
        }
        else {
            showErrorMessage();
        }
    });
}

//fillDataTable();

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

$(document).on('submit', 'form.public-ajax-form', function (e) {
    debugger;
    e.preventDefault();
    var currentForm = $(this);
    var formAction = currentForm.attr('action');
    var functionName = currentForm.attr('call-function-in-the-end');
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
            showLoading();
        },
        success: function (data, status) {
            if (data.isSuccessful == false) {
                //var finalData = data.data != null ? data.data : [data.message];
                var finalData = data.data || [data.message];
                fillValidationForm(finalData, currentForm);
                showToastr('warning', data.message);
            }
            else {
                window[functionName](data.message, data.data);
            }
        },
        complete: function () {
            hideLoading();
        },
        error: function () {
            showErrorMessage();
        }
    });
});

function activatingPageCount() {
    $('#page-count-selectbox').change(function () {
        var pageCountValue = this.value;
        $('form.search-form-via-ajax input[name$="Pagination.PageCount"]').val(pageCountValue);
        $('form.search-form-via-ajax').submit();
    })
}

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
    currentForm.find('.search-form-submit-button').attr('disabled', 'disabled');
    currentForm.find('.search-form-submit-button span').removeClass('d-none');

    $('.data-table-loading').removeClass('d-none');
    $('.data-table-body').html('');
    $('[data-bs-toggle="tooltip"], .tooltip').tooltip("hide");
    $('#record-not-found-box').remove();
    $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data, status) {
        isMainPaginationClicked = false;
        isGotoPageClicked = false;
        // hide loading and activating button
        currentForm.find('.search-form-submit-button').removeAttr('disabled');
        currentForm.find('.search-form-submit-button span').addClass('d-none');

        $('.data-table-loading').addClass('d-none');

        if (status == 'success') {
            if (data.isSuccessful == false) {
                fillValidationForm(data.data, currentForm);
                showToastr('warning', data.message);
            }
            else {
                $('.data-table-place').append(data);
                activatingPagination();
                activatingGotoPage();
                activatingModalForm();
                activatingDeleteButtons();
                activatingPageCount();
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


// End Ajax operations

$.validator.setDefaults({
    ignore: [],
    // other default options
});

$('input[data-val-ltrdirection="true"]').attr('dir', 'ltr');
$('input[data-val-isimage]').attr('accept', 'image/*');

$('.image-preivew-input').change(function () {
    var selectedFile = this.files[0];
    console.log(selectedFile);
    var imagePreviewBox = $(this).attr('image-preview-box');
    if (selectedFile && selectedFile.size > 0) {
        $(`#${imagePreviewBox}`).removeClass('d-none');
        $(`#${imagePreviewBox} img`).attr('src', URL.createObjectURL(selectedFile));
    } else {
        $(`#${imagePreviewBox} img`).attr('src', '');
        $(`#${imagePreviewBox}`).addClass('d-none');
    }
})