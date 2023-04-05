$(function () {
    // اگر فرم مشکل داشته باشه و از سمت سرور برگرده، این متغیر ترو میشه
    var hasValidationError = !isNullOrWhitespace($('form div:first li:first').text());
    if (hasValidationError) {
        showSweetAlert2('خطای اعتبار سنجی، لطفا خطا هایی که در بخش بالای درگاه نوشته شده اند را بررسی نمایید');
    }
    // موقعی که دکمه "اِن مرسوله" هاور شد
    // دراپ دان مِنو رو نشون بده
    // موقعی که بلر شد مخفیش کن
    $('#shipping-dropdown').hover(function () {
        $(this).dropdown('show');
    },
        function () {
            $(this).dropdown('hide');
        });

    // همینکه روی دکمه افزودن کد تخفیف کلیک شد
    // باکس افزودن کد تخفیف رو نمایش میده
    $('#show-discount-box-el-in-payment').click(function () {
        $(this).remove();
        $('#discount-code-box-in-payment').removeClass('d-none');
        $('#discount-code-box-in-payment input').focus();
    });

    // همینکه روی دکمه افزودن کارت هدیه کلیک شد
    // باکس افزودن کارت هدیه رو نمایش میده
    $('#show-gift-card-box-el-in-payment').click(function () {
        $(this).remove();
        $('#gift-card-code-box-in-payment').removeClass('d-none');
        $('#gift-card-code-box-in-payment input').focus();
    });

    var discountCodeValue = $('form input[name="CreateOrderAndPayModel.DiscountCode"]').val();
    // اگر فرم رفت سمت سرور و کد تخفیف مشکل داشته باشه، دوباره صفحه نمایش داده میشه و چون داخل اینپوت کد تخفیف مقدار هست
    // در نتیجه باید کلمه کد تخفیف رو دوباره داخل اینپوت کد تخفیف نمایش بدیم که کاربر بدونه کد تخفیف اشتباهی که وارد کرده بود چی بوده
    if (!isNullOrWhitespace(discountCodeValue)) {
        $('#discount-code-box-in-payment input').val(discountCodeValue);
        $('#show-discount-box-el-in-payment').click();
    }

    // اگر در داخل اینپوت کد تخفیف دکمه
    // enter
    // رو زد، به سمت سرور درخواست میفرستیم
    $('#discount-code-box-in-payment input').keypress(function (e) {
        if (e.which === 13) {
            $('#add-discount-code-button-in-payment').click();
        }
    });

    // اگر در داخل اینپوت کارت هدیه دکمه
    // enter
    // رو زد، به سمت سرور درخواست میفرستیم
    $('#gift-card-code-box-in-payment input').keypress(function (e) {
        if (e.which === 13) {
            $('#add-gift-card-code-button-in-payment').click();
        }
    });

    // اگر روی دکمه ثبت کد تخفیف کلیک شد، کد تخفیف رو به سمت سرور میفرسته که بررسی کنه همچین کدی وجود داره یا نه
    $('#add-discount-code-button-in-payment').click(function () {
        var code = $(this).parent().find('input').val();
        // اگر هنگام وارد کردن کد تخفیف، کارت هدیه، مبلغ قابل پرداخت رو صفر کرده بود، نباید اجازه بدیم
        // که از کد تخفیف استفاده کنه چون دلیل وجود نداره اگه مبلغ قابل پرداخت صفر باشه و بخوایم از کد تخفیف هم استفاده کنیم
        var finalPriceWithDiscount = finalPrice - giftCardCodePrice;
        
        if (isNullOrWhitespace(code)) {
            showSweetAlert2('کد تخفیف نباید خالی باشد');
        }
        else if (finalPriceWithDiscount <= 0) {
            showSweetAlert2('مبلغ قابل پرداخت ۰ تومان است و امکان استفاده از کد تخفیف وجود ندارد');
        }
        else {
            getDataWithAJAX('?handler=CheckForDiscount', { DiscountCode: code, SumPriceOfCart: finalPrice }, 'calculateDiscount');
        }
    });

    // اگر روی دکمه ثبت کارت هدیه کلیک شد، کارت هدیه رو به سمت سرور میفرسته که بررسی کنه همچین کارت هدیه ایی وجود داره یا نه
    $('#add-gift-card-code-button-in-payment').click(function () {
        var code = $(this).parent().find('input').val();
        // اگر هنگام وارد کردن کارت هدیه، کد تخفیف، مبلغ قابل پرداخت رو صفر کرده بود، نباید اجازه بدیم
        // که از کارت هدیه استفاده کنه چون دلیل وجود نداره اگه مبلغ قابل پرداخت صفر باشه و بخوایم از کارت هدیه هم استفاده کنیم
        var finalPriceWithDiscount = finalPrice - discountCodePrice;

        debugger;
        if (isNullOrWhitespace(code)) {
            showSweetAlert2('کارت هدیه نباید خالی باشد');
        }
        else if (finalPriceWithDiscount <= 0) {
            showSweetAlert2('مبلغ قابل پرداخت ۰ تومان است و امکان استفاده از کارت هدیه وجود ندارد');
        }
        else {
            getDataWithAJAX('?handler=CheckForGiftCard', { GiftCardCode: code, SumPriceOfCart: finalPrice }, 'calculateGiftCard');
        }
    });

    // اگر روی دکمه ضربدر کلیک شد باید کد تخفیف یا کارت هدیه رو حذف کنیم
    $('#remove-discount-code-button-in-payment, #remove-gift-card-code-button-in-payment').click(function () {
        if ($(this).attr('id').indexOf('gift-card') === -1) {
            removeDiscountAndGiftCard(this, true);
        } else {
            removeDiscountAndGiftCard(this, false);
        }
    });
});

function removeDiscountAndGiftCard(el, isDiscount) {
    var input = isDiscount ? 'Discount' : 'GiftCard';
    var elName = isDiscount ? 'discount' : 'gift-card';

    // مقدار اینپوت کد تخفیف یا کارت هدیه رو حذف میکنیم
    // که موقعی که فرم به سمت سرور ارسال میشه، کد تخفیف یا کارت هدیه مقدار نداشته باشن
    $('form').find('input[name="CreateOrderAndPayModel.' + input + 'Code"]').val('');

    // مخفی کردن دکمه ضربدر
    $(el).addClass('d-none');
    // نمایش دکمه ثبت
    $(el).prev('button').removeClass('d-none');
    // اینپوت کد
    var inputEl = $(el).parent().find('input');
    inputEl.val('');
    inputEl.focus();
    // مخفی کردن متن زیر اینپوت که میگه: کد تخفیف یا کارت هدیه اعمال شد
    $('#' + elName + '-code-added-text-in-payment').addClass('d-none');
    // مخفی کردن بخش کد تخفیف یا کارت هدیه در سمت چپ صفحه
    $('#' + elName + '-code-price-box-in-payment').parents('.d-flex').addClass('d-none');
    // اگر کد تخفیف و کارت هدیه نداشته باشیم کل بخش تخفیفات رو مخفی میکنیم
    // برای مواقعی که سه دیو مسقتیم داریم و خود کالا ها نیز تخفیف دارند
    if ($('#discounts-box-in-payment > div').length === 3) {
        if ($('#discounts-box-in-payment > div:eq(1)').hasClass('d-none') &&
            $('#discounts-box-in-payment > div:last').hasClass('d-none')) {
            $('#discounts-box-in-payment').addClass('d-none');
        }
    }
    // برای حالتی که خود کالا ها تخفیف ندارند و کلا دو دیو مستقیم داریم
    else if ($('#discounts-box-in-payment > div:first').hasClass('d-none') &&
        $('#discounts-box-in-payment > div:last').hasClass('d-none')) {
        $('#discounts-box-in-payment').addClass('d-none');
    }
    // محاسبه مجدد قیمت ها
    calculatePrices(0, isDiscount);
}

// بعد از اینکه کد تخیف رو به سمت سرور فرستادیم، نتیجه به این فانکشن برگشت داده میشه
function calculateDiscount(message, data) {
    calculateDiscountAndGiftCard(data, true);
}

// بعد از اینکه کارت هدیه رو به سمت سرور فرستادیم، نتیجه به این فانکشن برگشت داده میشه
function calculateGiftCard(message, data) {
    calculateDiscountAndGiftCard(data, false);
}

function calculateDiscountAndGiftCard(data, isDiscount) {
    var input = isDiscount ? 'Discount' : 'GiftCard';
    var elName = isDiscount ? 'discount' : 'gift-card';

    if (data.result) {
        // تغییر مقدار اینپوت که موقعی که دکمه پرداخت رو میزنیم
        // کد تخفیف یا کارت هدیه به سمت سرور ارسال بشه
        var code = $('#' + elName + '-code-box-in-payment').find('input').val();
        $('form').find('input[name="CreateOrderAndPayModel.' + input + 'Code"]').val(code);

        // اینپوت رو از حالت فوکس خارج میکنیم
        $('#' + elName + '-code-box-in-payment input').blur();
        // نمایش دکمه ضربدر
        $('#remove-' + elName + '-code-button-in-payment').removeClass('d-none');
        // مخفی کردن دکمه ثبت
        $('#add-' + elName + '-code-button-in-payment').addClass('d-none');
        // نمایش متن  "کد تخفیف یا کارت هدیه اعمال شد" در پایین اینپوت
        $('#' + elName + '-code-added-text-in-payment').removeClass('d-none');
        // نمایش دادن بخش تخفیف در سمت چپ صفحه، چه برای کارت هدیه چه برای کد تخفیف نمایش داده میشه
        // این باکس اصلیه تخفیفه چه برای کد تخفیف چه برای کارت هدیه
        // کل سکشن تخفیفات رو نمایش میدیم
        $('#discounts-box-in-payment').removeClass('d-none');
        // یا سکشن کارت هدیه یا کد تخفیف رو نمایش میدیم
        $('#' + elName + '-code-price-box-in-payment').parents('.d-flex').removeClass('d-none');
        // نمایش مقدار کارت هدیه یا کد تخفیف به صورت رقم فارسی
        $('#' + elName + '-code-price-box-in-payment').html(data.discountPrice.toString().addCommaToDigits().toPersinaDigit());
    } else {
        // مقدار اینپوت رو حذف میکنیم
        // که موقعی که فرم به سمت سرور ارسال میشه، مقدار نداشته باشه
        $('form').find('input[name="CreateOrderAndPayModel.' + input + 'Code"]').val('');

        // اینپوت رو دوباره به حالت فوکس در میاریم
        $('#' + elName + '-code-box-in-payment input').focus();
        // مخفی کردن دکمه ضربدر
        $('#remove-' + elName + '-code-button-in-payment').addClass('d-none');
        // نمایش دکمه ثبت
        $('#add-' + elName + '-code-button-in-payment').removeClass('d-none');
        // مخفی کردن متن "کد تخفیف یا کارت هدیه اعمال شد" در پایین اینپوت 
        $('#' + elName + '-code-added-text-in-payment').addClass('d-none');
        // مخفی کردن مقدار تخفیف در بخش چپ صفحه
        $('#show-' + elName + '-code-box-in-payment').addClass('d-none');
        showSweetAlert2(data.message);
    }

    // محاسبه مجدد قیمت ها بعد از اضافه شدن مقدار کد تخفیف
    calculatePrices(data.discountPrice, isDiscount);
}

// به محض لود صفحه این مقادیر رو میگیریم که در هنگام افزودن کد تخفیف بتونیم
// قیمت های جدید رو محاسبه کنیم
var totalPrice = parseInt($('#total-price-box-in-payment').html().trim().replace(/,/g, '').toEnglishDigit());
// تخفیف خود کالا ها
var discountPrice = parseInt($('#discount-price-box-in-payment span:last').html().trim().replace(/,/g, '').toEnglishDigit());
var finalPrice = totalPrice - discountPrice;
var discountCodePrice = 0;
var giftCardCodePrice = 0;

// محاسبه مجدد قیمت ها بعد از اضافه شدن مقدار کد تخفیف یا کارت هدیه
function calculatePrices(discountValue, isDiscount) {
    if (isDiscount) {
        discountCodePrice = discountValue;
    } else {
        giftCardCodePrice = discountValue;
    }

    // قیمت نهایی منهای میزان کد تخفیف منهای کارت هدیه که کاربر باید این مقدار رو پرداخت کنه
    // قیمت کل منهای تمامی تخفیفات
    var finalPriceWithDiscounts = finalPrice - discountCodePrice - giftCardCodePrice;

    // قابل پرداخت 10 هزاره، کد تخفیف یا کارت هدیه 20 هزاره، در نتیجه قابل پرداخت منفی میشه
    if (finalPriceWithDiscounts <= 0) {
        finalPriceWithDiscounts = 0;
    }

    // در قسمت قابل پرداخت مقدار متغیر بالایی رو نمایش میدیم
    $('#final-price-box-in-payment')
        .html(finalPriceWithDiscounts.toString().addCommaToDigits().toPersinaDigit());

    // اگر بخش "سود شما از خرید" به دلیل نبود تخفیف یا کارت هدیه مخفی بود، اون رو نمایش میدیم
    // که میزان کد تخفیف یا کارت هدیه رو در اونجا نمایش بدیم
    $('#discount-price-box-in-payment').removeClass('d-none');
    
    // سود شما از خرید
    // مقدار تخفیف خود کالا ها به علاوه مقدار کد تخفیف به علاوه مقدار کارت هدیه
    // جمع تمامی تخفیفات
    var allDiscounts = discountPrice + discountCodePrice + giftCardCodePrice;

    // نباید در بخش سود شما از خرید، مبلغی بیشتر از توتال پرایس نمایش داده بشه
    if (allDiscounts > totalPrice) {
        allDiscounts = totalPrice;
    }

    // سود شما از خرید
    $('#discount-price-box-in-payment span:last')
        .html(allDiscounts.toString().addCommaToDigits().toPersinaDigit());

    // محاسبه درصد تخفیف
    // کل توضیحات در ریزر پیج
    // Payment
    var totalPriceDivideBy100 = Math.ceil(totalPrice / 100);

    // چرا به پایین گرد  میکنیم ؟
    // چون تا زمانی که حتی یک تومان هم پرداخت کنیم، تخفیف، صد در صد نیست
    // برای مثال قیمت سبد خرید ده هزاره
    // حالا یک کد تخفیف
    // 9999
    // تومانی داریم، اگر محاسبه کنیم یک عدد بین 99 و 100 به دست میاد
    // و اگه به بالا گرد بشه میشه 100، درصورتیکه ما حداقل یک تومان پرداخت میکنیم پس نمیشه گفت که ما
    // تخفیف 100 درصدی داریم
    var percentageOfTotalPriceOfCartThatUserMustPay = Math.floor(finalPriceWithDiscounts / totalPriceDivideBy100);

    var discountPercentage = 100 - percentageOfTotalPriceOfCartThatUserMustPay;

    // اگر محصولات تخفیف نداشته باشن بخش سود شما از خرید نمایش داده نمیشه ومخفیه
    // و بعد اگه دکمه ضربدر رو بزنیم که کد تخفیف حذف بشه
    // باید دوباره بخش سود شما از خرید رو مخفی کنیم
    // چون از اول وجود نداشته و الان هم کد تخفیف نداریم که بخوایم بخش سود شما از خرید رو نمایش بدیم
    if (discountPercentage === 0) {
        $('#discount-price-box-in-payment').addClass('d-none');
    }

    // درصد تخفیف
    $('#discount-price-box-in-payment span:first')
        .html(discountPercentage.toString().toPersinaDigit());
}

// همینکه روی دکمه پرداخت کلیک بشه، دکمه رو غیر فعال کنیم که مجددا روی دکمه کلیک نشه
$('form').submit(function () {
    $('#create-order-and-pay-button').attr('disabled', 'disabled');
});