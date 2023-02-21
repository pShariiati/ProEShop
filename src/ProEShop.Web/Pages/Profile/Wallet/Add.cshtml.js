$(function () {
    // فارسی و سه رقم سه رقم نمایش دادن اعداد در اینپوت افزایش مقدار به کیف پول
    $('#add-wallet-input-in-wallet').on('keypress', function (event) {
        // اگه چیزی غیر از اعداد فارسی و انگلیسی رو وارد کنه باید جلوش رو بگیریم
        var regex = new RegExp("^[\\d۰-۹]$");
        var key = String.fromCharCode(event.which);
        if (!regex.test(key)) {
            return false;
        }

        // طول رشته داخل اینپوت قبل از اینکه سه رقم سه رقم جداش کنیم
        var valueLengthBeforeChange = this.value.length;
        var cursorPosStart = event.target.selectionStart;
        // اگر یک یا چند رقم رو سلکت کنه، این مورد با پوزیشن استارت متفاوت میشه
        // اما اگه چیزی رو سلکت نکنه، پوزیشن استارت و اند با هم برابر هستن
        var cursorPosEnd = event.target.selectionEnd;

        // ارقام قبل پوزیشن استارت
        var textBefore = this.value.substring(0, cursorPosStart);
        // ارقام بعد پوزیشن اند
        var textAfter = this.value.substring(cursorPosEnd, valueLengthBeforeChange);
        // تعداد اسلش های مقدار اینپوت قبل تغییر
        var slashCountBeforeChange = (this.value.match(/\//g) || []).length;

        // چرا مقدار داخل اینپوت رو دوباره به انگلیسی تبدیل میکنیم ؟
        // چون برای جدا سازی با اسلش باید اعداد انگلیسی رو به متد پاس بدیم
        this.value = (textBefore + key.toPersinaDigit() + textAfter)
            .replace(/\//g, '')
            .toEnglishDigit()
            .addSlashToDigits()
            .toPersinaDigit();

        // نباید کاربر بیشتر از 10 رقم وارد کند
        if (this.value.length > 10) {
            this.value = this.value
                .substring(0, 10)
                .replace(/\//g, '')
                .toEnglishDigit()
                .addSlashToDigits()
                .toPersinaDigit();;
            return false;
        }

        changeWalletValue(this.value);

        // طول رشته داخل اینپوت بعد از اینکه سه رقم سه رقم جداش کردیم
        var valueLengthAfterChange = this.value.length;
        // تعداد اسلش های مقدار اینپوت بعد تغییر
        var slashCountAfterChange = (this.value.match(/\//g) || []).length;

        // آیا متنی رو سلکت کرده ؟
        var isTextSelected = cursorPosStart !== cursorPosEnd;

        // تغییر پوزیشن کرسر

        // وارد کردن یک عدد
        // به شرطی که وارد کردن آن عدد موجب افزایش اسلش نشود
        // کرسر بعد از عدد وارد شده قرار میگیره
        if (valueLengthBeforeChange + 1 === valueLengthAfterChange) {
            this.setSelectionRange(cursorPosStart + 1, cursorPosStart + 1);
        }
        // در صورتیکه کاربر فقط یک عدد را سلکت کند و بعد یک عدد دیگر را وارد کند
        // طول رشته قبلی با جدید برابر خواهد بود
        // کرسر بعد از عدد وارد شده قرار میگیره
        else if (valueLengthBeforeChange === valueLengthAfterChange) {
            this.setSelectionRange(cursorPosStart + 1, cursorPosStart + 1);
        }
        // اگر متنی را سلکت کرده بود و بعد تعداد اسلش ها نیز تغییر پیدا نکند
        // کرسر بعد از عدد وارد شده قرار میگیره
        else if (isTextSelected && slashCountBeforeChange === slashCountAfterChange) {
            this.setSelectionRange(cursorPosStart + 1, cursorPosStart + 1);
        }

        // اگه ریترن فالس رو انجام ندیم، دکمه ایی که فشار دادیم رو در اینپوت نمایش میده
        // چرا ریترن فالس کردیم ؟ چون خودمون دکمه ایی که وارد شده رو در اینپوت به صورت فارسی نمایش میدیم
        // و دیگه نیازی نیست خود مرورگر دکمه ایی که فشار دادیم رو باز هم در اینپوت نمایش بده
        return false;
    }).on('keydown', function (event) {
        // چون کی پرس برای دکمه های دیلیت و بک اسپیس کار نمیکنه از این ایونت استفاده میکنیم
        // https://stackoverflow.com/questions/4690330/jquery-keypress-backspace-wont-fire

        // آیا دکمه دیلیت رو فشار داده
        var isDeletePressed = event.which === 46;
        if (event.which === 8 || isDeletePressed) {
            // اگه مقدار داخل اینپوت خالی باشه نیازی نیست کاری کنیم
            if (this.value.length > 0) {
                var cursorPosStart = event.target.selectionStart;
                var cursorPosEnd = event.target.selectionEnd;
                var textBefore = this.value.substring(0, cursorPosStart);
                var textAfter = this.value.substring(cursorPosEnd, this.value.length);
                // تعداد اسلش های مقدار اینپوت قبل تغییر
                var slashCountBeforeChange = (this.value.match(/\//g) || []).length;

                var selectedText;
                // اگر متنی رو سلکت کرده باشه
                if (cursorPosStart !== cursorPosEnd) {
                    selectedText = this.value.substring(cursorPosStart, cursorPosEnd);
                } else if (isDeletePressed) {
                    selectedText = textAfter[0];
                } else {
                    selectedText = textBefore.slice(-1);
                }

                if (selectedText === '/') {
                    return false;
                }

                // اگه متنی رو سلکت نکرده باشه
                // دیلیت: حذف اولین عدد از سمت راست
                // بک اسپیس: حذف آخرین عدد از سمت چپ

                // اگه پوزیشن استارت و اند برابر باشن یعنی متنی رو سلکت نکرده
                if (cursorPosStart === cursorPosEnd) {
                    if (isDeletePressed) {
                        textAfter = textAfter.substring(1, textBefore.length);
                    } else {
                        textBefore = textBefore.substring(0, textBefore.length - 1);
                    }
                }

                this.value = (textBefore + textAfter)
                    .replace(/\//g, '')
                    .toEnglishDigit()
                    .addSlashToDigits()
                    .toPersinaDigit();

                changeWalletValue(this.value);

                // تعداد اسلش های مقدار اینپوت بعد تغییر
                var slashCountAfterChange = (this.value.match(/\//g) || []).length;

                // تغییر پوزیشن کرسر
                // کرسر در مکان عدد یا اعداد حذف شده قرار میگیره
                // textBefore.length > 0
                if (textBefore.indexOf('/') === -1 && slashCountAfterChange === slashCountBeforeChange) {
                    this.setSelectionRange(cursorPosStart - 1, cursorPosStart - 1);
                }

                // چرا ریترن فالس کردم ؟
                // چون نمیخواد مرورگر چیزی رو پاک کنه
                // خودم پاکش میکنم
                // این ریتر فالس باعث میشه اگه دکمه ایی رو فشار بدم
                // مرورگر کار پیشرفرض اون دکمه رو انجام نده
                // برای مثال اگه دکمه بک اسپیس رو فشار بدم دیگه چیزی پاک نمیشه
                // خودم به جای مرورگر پاک میکنم
                return false;
            }
        }
    }).on('paste drop', function () {
        var input = this;
        setTimeout(function() {
            input.value = input.value
                .replace(/[^\d۰-۹]/g, '')
                .replace(/\//g, '')
                .toEnglishDigit()
                .addSlashToDigits()
                .toPersinaDigit();

            // نباید کاربر بیشتر از 10 رقم وارد کند
            if (input.value.length > 10) {
                input.value = input.value
                    .substring(0, 10)
                    .replace(/\//g, '')
                    .toEnglishDigit()
                    .addSlashToDigits()
                    .toPersinaDigit();;
                return false;
            }
            changeWalletValue(input.value);
        }, 0);
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