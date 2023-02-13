//__RequestVerificationToken
var rvt = '__RequestVerificationToken';

var htmlModalPlace = `<div class="modal fade" id="html-modal-place" data-bs-backdrop="static">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title"></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body"></div>
            <div class="modal-footer d-flex justify-content-start">
                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">بستن</button>
            </div>
        </div>
    </div>
</div>`;

var htmlScrollableModalPlace = `<div class="modal fade" id="html-scrollable-modal-place" data-bs-backdrop="static">
    <div class="modal-dialog modal-dialog-scrollable modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title"></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body"></div>
            <div class="modal-footer d-flex justify-content-start">
                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">بستن</button>
            </div>
        </div>
    </div>
</div>`;

var secondHtmlModalPlace = `<div class="modal fade" id="second-html-modal-place" data-bs-backdrop="static">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title"></h5>
                <button type="button" class="btn-close" data-bs-target="#html-modal-place" data-bs-toggle="modal" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body"></div>
            <div class="modal-footer d-flex justify-content-start">
                <button type="button" class="btn btn-danger" data-bs-target="#html-modal-place" data-bs-toggle="modal" data-bs-dismiss="modal">بستن</button>
            </div>
        </div>
    </div>
</div>`;

function addModalHeader(modal, headerText) {
    modal.find('.modal-header h5').html(headerText);
}

function closeHtmlModal() {
    $('#html-modal-place').modal('hide');
}

function closeScrollableHtmlModal() {
    $('#html-scrollable-modal-place').modal('hide');
}

function appendHtmlModalPlaceToBody(customClass = 'modal-xl', showInCenter = false, backdropStatic = true) {
    if (customClass === 'normal') {
        customClass = '';
    }

    if ($('#html-modal-place').length === 0) {
        $('body').append(htmlModalPlace);
    }

    $('#html-modal-place').removeAttr('data-bs-backdrop');
    $('#html-modal-place div:first').removeClass('modal-sm modal-lg modal-xl modal-dialog-centered');
    $('#html-modal-place div:first').addClass(customClass);
    if (showInCenter) {
        $('#html-modal-place div:first').addClass('modal-dialog-centered');
    }
    if (backdropStatic) {
        $('#html-modal-place').attr('data-bs-backdrop', 'static');
    }
}

function appendHtmlScrollableModalPlaceToBody(customClass = 'modal-xl', showInCenter = false, backdropStatic = true) {
    if (customClass === 'normal') {
        customClass = '';
    }

    if ($('#html-scrollable-modal-place').length === 0) {
        $('body').append(htmlScrollableModalPlace);
    }

    $('#html-scrollable-modal-place').removeAttr('data-bs-backdrop');
    $('#html-scrollable-modal-place div:first').removeClass('modal-sm modal-lg modal-xl modal-dialog-centered');
    $('#html-scrollable-modal-place div:first').addClass(customClass);
    if (showInCenter) {
        $('#html-scrollable-modal-place div:first').addClass('modal-dialog-centered');
    }
    if (backdropStatic) {
        $('#html-scrollable-modal-place').attr('data-bs-backdrop', 'static');
    }
}

function appendSecondHtmlModalPlaceToBody() {
    if ($('#second-html-modal-place').length === 0) {
        $('body').append(secondHtmlModalPlace);
    }
}

var formModalPlace = `<div class="modal fade" id="form-modal-place" data-bs-backdrop="static">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title"></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body"></div>
            <div class="modal-footer d-flex justify-content-start">
                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">بستن</button>
            </div>
        </div>
    </div>
</div>`;

// Show sweet alert

function showSweetAlert2(text, functionToCallAfterConfirm, functionToCallAfterReject) {
    Swal.fire({
        title: 'اعلان',
        text: text,
        icon: 'warning',
        confirmButtonText: 'بله',
        showDenyButton: true,
        denyButtonText: 'خیر',
        confirmButtonColor: '#067719',
        allowOutsideClick: false
    }).then((result) => {
        if (result.isConfirmed) {
            window[functionToCallAfterConfirm]();
        } else {
            window[functionToCallAfterReject]();
        }
    });
}

// End show sweet alert

function appendFormModalPlaceToBody() {
    if ($('#form-modal-place').length === 0) {
        $('body').append(formModalPlace);
    }
}
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
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('.data-table-place [data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl,
            {
                trigger: 'hover'
            });
    });
}

// Enabling tooltips
function enablingNormalTooltips() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl,
            {
                trigger: 'hover'
            });
    });
}

function showErrorMessage(message) {
    showToastr('error', message != null ? message : 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
}

// Send `TinyMCE` images to server with specific url
function sendTinyMceImagesToServer(blobInfo, success, failure, progress, url) {
    var formData = new FormData();
    formData.append('file', blobInfo.blob(), blobInfo.filename());
    formData.append(rvt, $('textarea.custom-tinymce:first').parents('form').find('input[name="' + rvt + '"]').val());
    $.ajax({
        url: `${location.pathname}?handler=${url}`,
        data: formData,
        type: 'POST',
        enctype: 'multipart/form-data',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            if (data === false) {
                failure('خطایی به وجود آمد');
            } else {
                success(data.location);
            }
        },
        error: function () {
            failure('خطایی به وجود آمد');
        }
    });
};

function initializeTinyMCE() {
    $('textarea.custom-tinymce').each(function () {
        var textareaId = `#${$(this).attr('id')}`;
        tinymce.remove(textareaId);
        tinymce.init({
            selector: textareaId,
            setup: function (editor) {
                editor.on('blur', function (e) {
                    var elementId = $(e.target.targetElm).attr('id');
                    $(e.target.formElement).validate().element(`#${elementId}`);
                });
            },
            min_height: 300,
            max_height: 500,
            language: 'fa_IR',
            language_url: '/js/fa_IR.js',
            content_style: 'body {font-family: Vazir}',
            plugins: 'link table preview wordcount autoresize',
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
    });
}

// برای نمایش ادیتور
// Tinymce
// از این کد استفاده میکنیم
document.addEventListener('focusin', function (e) {
    if (e.target.closest('.tox-tinymce-aux, .moxman-window, .tam-assetmanager-root') !== null) {
        e.stopImmediatePropagation();
    }
});

function initializeSelect2() {
    if ($('.modal .custom-select2').length > 0) {
        $('.modal .custom-select2').select2({
            theme: 'bootstrap-5',
            dropdownParent: $('#form-modal-place'),
            width: '100%'
        });
    }
}

function initializeSelect2WithoutModal() {
    if ($('.custom-select2').length > 0) {
        $('.custom-select2').select2({
            theme: 'bootstrap-5',
            width: '100%'
        });
    }
}

// یک آرایه و یک آیتم میگیره
// آیتم رو از آرایه حذف میکنه
function removeItemInArray(arr, item) {
    var found = arr.indexOf(item);

    while (found !== -1) {
        arr.splice(found, 1);
        found = arr.indexOf(item);
    }
}

// Validation
if (jQuery.validator) {

    // برای اعتبار سنجی اینپوت های مخفی از این کد استفاده میکنیم
    // کجا کاربر داره ؟
    // هنگام استفاده از
    // navs-tabs
    // What is navs-tabs ?
    // https://getbootstrap.com/docs/5.0/components/navs-tabs/#javascript-behavior
    // برای مثال در تب دوم، اینپوت های تب اول مخفی هستن
    // و به صورت پیشفرض اعتبار سنجی نمیشن
    // کد پایین، اینپوت های مخفی رو هم اعتبار سنجی میکنه
    $.validator.setDefaults({
        ignore: []
    });

    // اگر چکباکس تیک نخورده باشد
    // متن خطا را نمایش میدهد
    var defaultRangeValidator = $.validator.methods.range;

    $.validator.methods.range = function (value, element, param) {
        if (element.type === 'checkbox') {
            return element.checked;
        } else {
            return defaultRangeValidator.call(this, value, element, param);
        }
    }

    // fileRequired
    jQuery.validator.addMethod("fileRequired", function (value, element, param) {
        var filesLength = element.files.length;
        if (filesLength > 0) {
            for (var i = 0; i < filesLength; i++) {
                if (element.files[0].size === 0) {
                    return false;
                }
            }
            return true;
        }
        return false;
    });
    jQuery.validator.unobtrusive.adapters.addBool("fileRequired");

    // allowExtensions
    jQuery.validator.addMethod('allowExtensions', function (value, element, param) {
        var selectedFiles = element.files;
        if (selectedFiles[0] === undefined) {
            return true;
        }
        var whiteListExtensions = $(element).data('val-whitelistextensions').split(',');
        for (var counter = 0; counter < selectedFiles.length; counter++) {
            var currentFile = selectedFiles[counter];
            if (currentFile != null) {
                if (!whiteListExtensions.includes(currentFile.type))
                    return false;
            }
        }
        return true;
    });
    jQuery.validator.unobtrusive.adapters.addBool('allowExtensions');


    var imageInputsWithProblems = [];
    // isImage
    jQuery.validator.addMethod('isImage', function (value, element, param) {
        var selectedFiles = element.files;
        if (selectedFiles[0] === undefined) {
            return true;
        }
        var whiteListExtensions = $(element).data('val-whitelistextensions').split(',');
        for (var counter = 0; counter < selectedFiles.length; counter++) {
            if (!whiteListExtensions.includes(selectedFiles[counter].type)) {
                return false;
            }
        }

        /////
        var currentElementId = $(element).attr('id');
        var currentForm = $(element).parents('form');

        if (imageInputsWithProblems.includes(currentElementId)) {
            removeItemInArray(imageInputsWithProblems, currentElementId);
            return false;
        }
        $('[id^="image-preview-box-temp"]').remove();
        for (var i = 0; i < selectedFiles.length; i++) {
            $('body').append(`<img class="d-none" id="image-preview-box-temp-${i}" />`);
        }
        for (var j = 0; j < selectedFiles.length; j++) {
            $(`#image-preview-box-temp-${j}`).attr('src', URL.createObjectURL(selectedFiles[j]));
            $(`#image-preview-box-temp-${j}`).off('error');
            $(`#image-preview-box-temp-${j}`).on('error',
                function () {
                    imageInputsWithProblems.push(currentElementId);
                    currentForm.validate().element(`#${currentElementId}`);
                });
        }
        return true;
    });
    jQuery.validator.unobtrusive.adapters.addBool('isImage');

    // maxFileSize
    jQuery.validator.addMethod('maxFileSize', function (value, element, param) {
        var selectedFiles = element.files;
        if (selectedFiles[0] === undefined) {
            return true;
        }
        var maxFileSize = $(element).data('val-maxsize');
        for (var counter = 0; counter < selectedFiles.length; counter++) {
            var currentFile = selectedFiles[counter];
            if (currentFile != null) {
                var currentFileSize = currentFile.size;
                if (currentFileSize > maxFileSize)
                    return false;
            }
        }
        return true;
    });
    jQuery.validator.unobtrusive.adapters.addBool('maxFileSize');

    // makeTinyMceRequired
    jQuery.validator.addMethod('makeTinyMceRequired', function (value, element, param) {
        var editorId = $(element).attr('id');
        var editorContent = tinyMCE.get(editorId).getContent();
        $('body').append(`<div id="test-makeTinyMceRequired">${editorContent}</div>`);
        var result = isNullOrWhitespace($('#test-makeTinyMceRequired').text());
        $('#test-makeTinyMceRequired').remove();
        return !result;
    });
    jQuery.validator.unobtrusive.adapters.addBool('makeTinyMceRequired');

    // divisibleBy10
    jQuery.validator.addMethod('divisibleBy10', function (value, element, param) {
        var price = $(element).val();
        if (!price)
            return true;
        return price % 10 === 0;
    });
    jQuery.validator.unobtrusive.adapters.addBool('divisibleBy10');
}

// End validation

function isNullOrWhitespace(input) {

    if (typeof input === 'undefined' || input == null) return true;

    return input.replace(/\s/g, '').length < 1;
}

// Ajax operations

// فعال ساز دکمه حذف، در داخل گرید
function activatingDeleteButtons(isModalMode) {
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
                $.post(currentForm.attr('action'), formData, function (data) {
                    if (data.isSuccessful === false) {
                        showToastr('warning', data.message);
                    }
                    else {
                        if (isModalMode) {
                            $('#html-modal-place').modal('hide');
                        }
                        showToastr('success', data.message);
                        fillDataTable();
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
    $('.autocomplete').each(function () {
        var currentSearchUrl = $(this).attr('autocomplete-search-url');
        var currentId = $(this).attr('id');
        $(`#${currentId}`).autocomplete({
            source: currentSearchUrl,
            minLength: 2,
            delay: 500,
            select: function (event, ui) {
                if (typeof window['onAutocompleteSelect'] === 'function') {
                    window['onAutocompleteSelect'](event, ui);
                }
            }
        });
    });
}

// این فانکشن فرم های مربوط به ایجاد و ویرایش را
// به صورت ایجکس برگشت میزند که در داخل مودال نمایش دهیم
function activatingModalForm() {
    $('.show-modal-form-button').click(function (e) {
        e.preventDefault();
        var urlToLoadTheForm = $(this).attr('href');
        var customTitle = $(this).attr('custom-title');
        var functionNameToCallInTheEnd = $(this).attr('functionNameToCallInTheEnd');
        if (customTitle == undefined) {
            customTitle = $(this).text().trim();
        }
        appendFormModalPlaceToBody();
        $('#form-modal-place .modal-header h5').html(customTitle);
        showLoading();
        $.get(urlToLoadTheForm, function (data) {
            if (data.isSuccessful === false) {
                showToastr('warning', data.message);
            }
            else {
                $('#form-modal-place .modal-body').html(data);
                initializeTinyMCE();
                initializeSelect2();
                initializingAutocomplete();
                activatingInputAttributes();
                $.validator.unobtrusive.parse($('#form-modal-place form'));
                if (typeof window[functionNameToCallInTheEnd] === 'function') {
                    window[functionNameToCallInTheEnd](data);
                }
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

// فعال ساز مربوط به صحفه بندی
function activatingPagination() {
    $('#main-pagination button').not('.active').click(function () {
        isMainPaginationClicked = true;
        var currentPageSelected = $(this).val();
        $('.search-form-via-ajax input[name$="Pagination.CurrentPage"]').val(currentPageSelected);
        $('.search-form-via-ajax').submit();
    });
}

// فعال ساز دکمه برو به صفحه فلان
function activatingGotoPage() {
    $('#go-to-page-button').click(function () {
        isGotoPageClicked = true;
    });
}

// خواندن اطلاعات و ریختن آن در داخل گرید
function fillDataTable() {
    $('.data-table-place .data-table-body').remove();
    $('.search-form-submit-button').attr('disabled', 'disabled');
    $('.data-table-loading').removeClass('d-none');
    $('#record-not-found-box').remove();

    var currentForm = $('form.search-form-via-ajax');
    const formData = currentForm.serializeArray();

    $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data) {
        if (data.isSuccessful === false) {
            fillValidationForm(data.data, currentForm);
            showToastr('warning', data.message);
        } else {
            $('.data-table-place').append(data);
            activatingPagination();
            activatingGotoPage();
            activatingModalForm();
            activatingDeleteButtons();
            activatingPageCount();
            enablingTooltips();
            convertEnglishNumbersToPersianNumber();
        }
    }).fail(function () {
        showErrorMessage();
    }).always(function () {
        $('.search-form-submit-button').removeAttr('disabled');
        $('.data-table-loading').addClass('d-none');
    });
}

$(document).on('click',
    '.get-html-with-ajax',
    function () {
        var funcToCall = $(this).attr('functionNameToCallOnClick');
        window[funcToCall](this);
    });

//fillDataTable();

// فرم ایجاد و ویرایش در داخل مودال موقعی که سابمیت شوند توسط این
// فانکشن به صورت ایجکسی به سمت سرور ارسال میشوند
$(document).on('submit', 'form.custom-ajax-form', function (e) {
    e.preventDefault();
    var currentForm = $(this);
    var closeWhenDone = currentForm.attr('close-when-done');
    var callFunctionInTheEnd = currentForm.attr('call-function-in-the-end');
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
        success: function (data) {
            if (data.isSuccessful === false) {
                // نمایش دادن خطاهایی که از سمت سرور اومدن در بخش 
                // Validation summary
                var finalData = data.data || [data.message];
                fillValidationForm(finalData, currentForm);
                showToastr('warning', data.message);
            }
            else {
                if (callFunctionInTheEnd) {
                    customAjaxFormFunction(data);
                }
                fillDataTable();
                if (closeWhenDone !== 'false') {
                    $('#form-modal-place').modal('hide');
                }
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

if (jQuery.validator) {
    // به محض بلر شدن یک اینپوت
    // تمامی اینپوت های فرم را مجددا اعتبار سنجی میکند
    // چرا از این استفاده میکنیم ؟
    // برای مثال شما روی دکمه ثبت نام کلیک میکنید و
    // در بالای صفحه و داخل تگ
    // <div asp-validation-summary="All" class="text-danger"></div>
    // مینویسد ایمیل را وارد کنید
    // شما نیز ایمیل را وارد میکنید
    // اما در قسمت بالای صفحه همچنان متن "لطفا ایمیل را وارد کنید" وجود دارد
    // برای اینکه این مشکل حل شود از این کد استفاده میکنیم
    $(document).on('blur', 'form input', function () {
        var currentForm = $(this).parents('form');
        currentForm.valid();
        if (currentForm.valid()) {
            currentForm.find('div[class*="validation-summary"] ul').html('');
        }
    });

    $(document).on('change',
        'form input.custom-md-persian-datepicker, form select, form input[type="checkbox"], form input[type="file"]',
        function () {
            $(this).parents('form').valid();
        });
}


// این فانکشن هر فرمی را به صورت پست به سمت سرور با استفاده از ایجکس
// ارسال میکند
$(document).on('submit', 'form.public-ajax-form', function (e) {
    e.preventDefault();
    var currentForm = this;
    var hideLoadingAttr = $(this).attr('hide-loading');

    $('#html-modal-place').modal('hide');
    $('#html-scrollable-modal-place').modal('hide');
    $('#second-html-modal-place').modal('hide');

    if (!hideLoadingAttr) {
        showLoading();
    }

    if ($(this).parents('.modal').length === 0) {
        publicAjaxFormFunction(currentForm);
    } else {
        $(this).parents('.modal').off('hidden.bs.modal').on('hidden.bs.modal', function () {
            publicAjaxFormFunction(currentForm);
        });
    }
});

function publicAjaxFormFunction(form) {
    var currentForm = $(form);
    var formAction = currentForm.attr('action');
    var functionName = currentForm.attr('functionNameToCallInTheEnd');
    var showModalInTheEnd = currentForm.attr('showModalInTheEnd');
    var formData = new FormData(form);
    var modalId = currentForm.parents('.modal').attr('id');

    $.ajax({
        url: formAction,
        data: formData,
        type: 'POST',
        enctype: 'multipart/form-data',
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.isSuccessful === false) {
                var finalData = data.data || [data.message];
                fillValidationForm(finalData, currentForm);
                showToastr('warning', data.message);
                if (modalId === 'second-html-modal-place') {
                    $(`#${modalId}`).modal('show');
                } else if (modalId === 'html-modal-place') {
                    $(`#${modalId}`).modal('show');
                }
                else if (modalId === 'html-scrollable-modal-place') {
                    $(`#${modalId}`).modal('show');
                }
            }
            else {
                if (showModalInTheEnd) {
                    if (modalId === 'second-html-modal-place') {
                        $(`#${modalId}`).modal('show');
                    } else if (modalId === 'html-modal-place') {
                        $(`#${modalId}`).modal('show');
                    }
                    else if (modalId === 'html-scrollable-modal-place') {
                        $(`#${modalId}`).modal('show');
                    }
                }

                window[functionName](data.message, data.data, form);
            }
        },
        complete: function () {
            hideLoading();
            // دکمه رو از حالت فوکس خارج میکنیم که اگر کاربر
            // دکمه اسپیس رو فشار داد، مجددا این فانکشن فراخوانی نشود
            currentForm.find('button:submit').blur();
            currentForm.parents('.modal').off('hidden.bs.modal');
        },
        error: function () {
            showErrorMessage();
        }
    });
}

// این فانکشن هر فرمی را به صورت پست به سمت سرور با استفاده از ایجکس
// ارسال میکند و یک صفحه اچ تی ام ال برگشت میزند
$(document).on('submit', '.get-html-by-sending-form', function (e) {
    e.preventDefault();
    var currentForm = $(this);
    var formAction = currentForm.attr('action');
    var functionName = currentForm.attr('functionNameToCallInTheEnd');
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
            $('#html-modal-place').modal('hide');
            showLoading();
        },
        success: function (data) {
            if (data.isSuccessful === false) {
                //var finalData = data.data != null ? data.data : [data.message];
                var finalData = data.data || [data.message];
                fillValidationForm(finalData, currentForm);
                showToastr('warning', data.message);
            }
            else {
                window[functionName](data.data);
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

// فعالساز مربوط به تعداد آیتم در هر صفحه
function activatingPageCount() {
    $('#page-count-selectbox').change(function () {
        var pageCountValue = this.value;
        $('form.search-form-via-ajax input[name$="Pagination.PageCount"]').val(pageCountValue);
        $('form.search-form-via-ajax').submit();
    });
}

// برای مثال در صفحه دو یک گرید هستیم
// و کاربر یک عبارتی را سرچ میکند ما باید بیاییم
// و از صفحه یک دوباره شروع به نمایش دادن اطلاعات کنیم
// این متغیر برای این کار است
var isMainPaginationClicked = false;

// اگر دکمه برو به فلان صحفحه کلیک شده بود
// باید به همان صفحه برویم
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
    $.get(`${location.pathname}?handler=GetDataTable`, formData, function (data) {
        isMainPaginationClicked = false;
        isGotoPageClicked = false;
        // hide loading and activating button
        currentForm.find('.search-form-submit-button').removeAttr('disabled');
        currentForm.find('.search-form-submit-button span').addClass('d-none');

        $('.data-table-loading').addClass('d-none');

        if (data.isSuccessful === false) {
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
            convertEnglishNumbersToPersianNumber();
        }
    }).fail(function () {
        showErrorMessage();
    }).always(function () {
        hideLoading();
    });
});

// موقعی که یک فرم به سمت سرور ارسال میشود
// اگر خطای اعتبار سنجی داشته باشد
// با استفاده از این فانکشن متن خطاها را داخل
// <div asp-validation-summary="All" class="text-danger"></div>
// نمایش میدهیم
function fillValidationForm(errors, currentForm) {
    var result = '<ul>';
    errors.forEach(function (e) {
        result += `<li>${e}</li>`;
    });
    result += '</ul>';
    currentForm.find('div[class*="validation-summary"]').html(result);
}

// با استفاده از این فانکشن میتوانیم اطلاعاتی را از سمت سرور دریافت کنیم
// برای مثال برای خواندن شهرستان های یک استان از این فانکشن استفاده میکنیم
function getDataWithAJAX(url, formData, functionNameToCallInTheEnd) {
    $.ajax({
        url: url,
        data: formData,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        beforeSend: function () {
            showLoading();
        },
        success: function (data) {
            if (data.isSuccessful === false) {
                showToastr('warning', data.message);
            }
            else {
                window[functionNameToCallInTheEnd](data.message, data.data);
            }
        },
        complete: function () {
            hideLoading();
        },
        error: function () {
            showErrorMessage();
        }
    });
}

// خواندن صفحات
// html
// از سمت سرور
function getHtmlWithAJAX(url, formData, functionNameToCallInTheEnd, clickedButton, loading = true) {
    if (clickedButton) {
        // دکمه رو از حالت فوکس خارج میکنیم که اگر کاربر
        // دکمه اسپیس رو فشار داد، مجددا این فانکشن فراخوانی نشود
        $(clickedButton).blur();
    }
    $.ajax({
        url: url,
        data: formData,
        type: 'GET',
        traditional: true,
        beforeSend: function () {
            if (loading) {
                showLoading();
            }
        },
        success: function (data) {
            if (data.isSuccessful === false) {
                showToastr('warning', data.message);
            } else {
                window[functionNameToCallInTheEnd](data, clickedButton);
            }
        },
        complete: function () {
            hideLoading();
        },
        error: function () {
            showErrorMessage();
        }
    });
}

// End Ajax operations

// به محض فراخوانی صفحه این فانکشن را فرخوانی میکنیم
// که اینپوت هایی که اتریبیوت های پایین را دارند
// اتریبیوت جاوا اسکریپتی مورد نظر برای آنها اضافه شود

function activatingInputAttributes() {
    // اگر به یک پراپرتی اتریبیوت
    // ltr
    // را بدهیم
    // این خط کد اینپوت مورد نظر را چپ به راست میکند
    $('input[data-val-ltrdirection="true"]').attr('dir', 'ltr');

    // فیلتر کردن ورودی های کاربر هنگام انتخاب فایل
    // فقط عکس هارو به کاربر نمایش میدیم
    $('input[data-val-isimage]').attr('accept', 'image/*');
}

// نمایش پیش نمایش عکس
$('.image-preview-input').change(function () {
    var selectedFile = this.files[0];
    var imagePreviewBox = $(this).attr('image-preview-box');
    if (selectedFile && selectedFile.size > 0) {
        $(`#${imagePreviewBox}`).removeClass('d-none');
        $(`#${imagePreviewBox} img`).attr('src', URL.createObjectURL(selectedFile));
    } else {
        $(`#${imagePreviewBox} img`).attr('src', '');
        $(`#${imagePreviewBox}`).addClass('d-none');
    }
});

// نمایش پیش نمایش عکس برای حالت چند عکسی
$('.multiple-images-preview-input').change(function () {
    var selectedFiles = this.files;
    var imagesPreviewBox = $(this).attr('images-preview-box');
    $(`#${imagesPreviewBox}`).html('');
    if (selectedFiles && selectedFiles.length > 0) {
        $(`#${imagesPreviewBox}`).removeClass('d-none');
        for (var i = 0; i < selectedFiles.length; i++) {
            $(`#${imagesPreviewBox}`).append('<div class="my-2"><img width="100" src="" /></div>');
            $(`#${imagesPreviewBox} img:last`).attr('src', URL.createObjectURL(selectedFiles[i]));
        }
    } else {
        $(`#${imagesPreviewBox}`).addClass('d-none');
    }
});

// Convert English numbers to Persian numbers
// https://seifzadeh.blog.ir/post/convert-number-javascript
String.prototype.toPersinaDigit = function () {
    var id = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    return this.replace(/[0-9]/g, function (w) {
        return id[+w];
    });
}

// Convert Perisan numbers to English numbers
String.prototype.toEnglishDigit = function () {
    var find = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    var replace = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    var replaceString = this; var regex;
    for (var i = 0; i < find.length; i++) {
        regex = new RegExp(find[i], "g"); replaceString = replaceString.replace(regex, replace[i]);
    }
    return replaceString;
};

// Add comma after 3 digits
String.prototype.addCommaToDigits = function () {
    return this.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
}

// Add slash after 3 digits
String.prototype.addSlashToDigits = function () {
    return this.replace(/\B(?=(\d{3})+(?!\d))/g, '/');
}

function fallbackCopyTextToClipboard(text, functionNameToCallInTheEnd, clickedEl) {
    var textArea = document.createElement('textarea');
    textArea.value = text;

    // Avoid scrolling to bottom
    textArea.style.top = '0';
    textArea.style.left = '0';
    textArea.style.position = 'fixed';

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
        if (!successful)
            showErrorMessage('مرورگر شما قابلیت کپی کردن متن را ندارد');
        else {
            if (typeof window[functionNameToCallInTheEnd] === 'function') {
                window[functionNameToCallInTheEnd](clickedEl);
            }
        }
    } catch (err) {
        showErrorMessage('مرورگر شما قابلیت کپی کردن متن را ندارد');
    }

    document.body.removeChild(textArea);
}
function copyTextToClipboard(text, functionNameToCallInTheEnd, clickedEl) {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(text, functionNameToCallInTheEnd, clickedEl);
        return;
    }
    navigator.clipboard.writeText(text).then(function () {
        if (typeof window[functionNameToCallInTheEnd] === 'function') {
            window[functionNameToCallInTheEnd](clickedEl);
        }
    }, function (err) {
        showErrorMessage('مرورگر شما قابلیت کپی کردن متن را ندارد');
    });
}

function convertEnglishNumbersToPersianNumber() {
    $('.persian-numbers').each(function () {
        var result = $(this).html().toPersinaDigit();
        $(this).html(result);
    });
}

// اسکرول به صورت انیمیت به یک المنت خاص
function scrollToEl(el, subtract = 0) {
    $('html, body').animate({
        scrollTop: $(el).offset().top - subtract
    }, 0);
}

// فعال سازی اعتبار سنجی داخل فرم مودال
function activatingModalFormValidation(modal) {
    $.validator.unobtrusive.parse(modal.find('form'));
}

$(function () {
    activatingInputAttributes();
    initializeSelect2WithoutModal();
    initializeTinyMCE();
    enablingNormalTooltips();
    convertEnglishNumbersToPersianNumber();

    $('textarea[add-image-plugin="true"]').each(function () {
        var elementId = $(this).attr('id');
        var currentTinyMce = tinymce.get(elementId);
        currentTinyMce.settings.plugins += ' image';
        currentTinyMce.settings.toolbar[4].items.push('image');
        currentTinyMce.settings.image_title = true;
    });

    // Initialize TinyMCE upload image plugin
    $('textarea.custom-tinymce').each(function () {
        var elementId = $(this).attr('id');
        var uploadImageUrl = $(this).attr('upload-image-url');
        var tinyMceInstance = tinymce.get(elementId);
        tinyMceInstance.settings.images_upload_handler = function (blobInfo, success, failure, progress) {
            sendTinyMceImagesToServer(blobInfo, success, failure, progress, uploadImageUrl);
        };
    });
});