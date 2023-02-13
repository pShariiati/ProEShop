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
    });
});