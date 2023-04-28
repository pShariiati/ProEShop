$(function () {
    // فارسی و سه رقم سه رقم نمایش دادن اعداد در اینپوت افزایش مقدار به کیف پول
    $('#add-wallet-input-in-wallet').on('keypress', function (event) {
        return keypressForPersianNumbersInPriceInput(event, this, false, 10, 'changeWalletValue');
    }).on('keydown', function (event) {
        return backspaceAndDeleteForPersianNumbersInPriceInput(event, this, false, 'changeWalletValue');
    }).on('paste drop', function () {
        customEventsForPersianNumbersInPriceInput(this, false, 10, 'changeWalletValue');
    });

    // کلیک روی مقادیر ثابت کیف پول
    $('#add-wallet-const-values-box div').click(function () {
        var divIndex = $(this).index();
        var addWalletInput = $('#add-wallet-input-in-wallet');
        if (divIndex === 0) {
            addWalletInput.val('۵۰/۰۰۰');
        }
        else if (divIndex === 1) {
            addWalletInput.val('۲۰۰/۰۰۰');
        } else {
            addWalletInput.val('۵۰۰/۰۰۰');
        }
        changeWalletValue(addWalletInput.val());
    });
});

// گرفتن مقدار داخل اینپوتی که کاربر تایپ کرده و بعد انتقال این مقدار به داخل اینپوت مبلغ
function changeWalletValue(value) {
    // ۵۰/۰۰۰ => 50000
    value = value.replace(/\//g, '')
        .toEnglishDigit();

    if (value === '50000') {
        changeConstValueBgOfWallet($('#add-wallet-const-values-box div:first'));
    }
    else if (value === '200000') {
        changeConstValueBgOfWallet($('#add-wallet-const-values-box div:eq(1)'));
    }
    else if (value === '500000') {
        changeConstValueBgOfWallet($('#add-wallet-const-values-box div:last'));
    } else {
        changeConstValueBgOfWallet();
    }

    if (value.length > 6) {
        $('#add-wallet-input-in-wallet').width(113);
    } else {
        $('#add-wallet-input-in-wallet').width(90);
    }

    var form = $('#add-value-to-wallet-form');
    form.find('input:hidden:first').val(value);
    if (form.valid()) {
        form.find('span:first').removeClass('text-danger');
        form.find('span:first').addClass('text-success');
        form.find('#add-wallet-input-in-wallet').parent().removeClass('border-danger');
        form.find('#add-wallet-input-in-wallet').parent().addClass('border-dark');
    } else {
        form.find('span:first').removeClass('text-success');
        form.find('span:first').addClass('text-danger');
        form.find('#add-wallet-input-in-wallet').parent().removeClass('border-dark');
        form.find('#add-wallet-input-in-wallet').parent().addClass('border-danger');
    }
}

function changeConstValueBgOfWallet(el) {
    $('#add-wallet-const-values-box div').removeAttr('id');
    $('#add-wallet-const-values-box div').removeClass('border-primary text-primary border-2');
    $('#add-wallet-const-values-box div').addClass('border-dark');

    if (el) {
        $(el).attr('id', 'add-wallet-const-values-bg');
        $(el).removeClass('border-dark');
        $(el).addClass('border-primary text-primary border-2');
    }
}