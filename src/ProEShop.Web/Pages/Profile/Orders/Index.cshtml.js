$(function () {
    activationSwiper();
    $('.item-in-profile-order').click(function (e) {
        if ($(e.target).parents('.register-return-product-button-in-profile-order').length) {
            e.preventDefault();
            var orderNumber = $(this).attr('href').split('/').at(-1);
            location.href = `/Profile/Orders/ReturnProduct/${orderNumber}`;
        }
    });
});

function activationSwiper() {
    new Swiper('.product-images-in-profile-orders', {
        slidesPerView: 8,
        spaceBetween: 30,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
        breakpoints: {
            0: {
                slidesPerView: 5,
                spaceBetween: 30
            },
            768: {
                slidesPerView: 8,
                spaceBetween: 30
            }
        }
    });
}

// نمایش محصولات به صورت صفحه بندی شده
function showOrdersByPagination(el) {
    // اگر در داخل صفحه یک هستیم، و دوباره روی صفحه یک کلیک کردیم نیازی به گرفتن
    // اطلاعات از سمت سرور نیست و نباید اجازه دهیم درخواستی به سمت سرور زده شود
    if ($(el).hasClass('bg-danger')) {
        return;
    }

    var pageNumber = $(el).attr('page-number');

    getHtmlWithAJAX('?handler=ShowOrdersByPagination', { pageNumber: pageNumber }, 'showOrdersByPaginationFunction');
}

// نمایش محصولات به صورت صفحه بندی شده
function showOrdersByPaginationFunction(data) {
    $('#orders-box-in-comment-profile').html(data);
    activationSwiper();
    convertEnglishNumbersToPersianNumber();
    scrollToEl('body');
}