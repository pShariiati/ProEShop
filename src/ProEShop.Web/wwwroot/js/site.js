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

// Enable tooltips
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});

function showErrorMessage(message) {
    showToastr('error', message != null ? message : 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
}

function initializeTinyMCE() {
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

// End validation