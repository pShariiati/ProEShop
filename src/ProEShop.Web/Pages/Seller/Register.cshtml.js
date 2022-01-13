(function ($) {
    $.fn.hasScrollBar = function () {
        return this.get(0).scrollHeight > this.height();
    }
})(jQuery);

function setScrollForRegisterSellerSideBar() {
    if (!$('#register-seller-right-side').hasScrollBar()) {
        $('#register-seller-right-side').removeClass('over-flow-scroll-y');
    }
    else {
        $('#register-seller-right-side').addClass('over-flow-scroll-y');
    }
}

$(window).on('resize', function () {
    setScrollForRegisterSellerSideBar();
});
setScrollForRegisterSellerSideBar();tScrollForRegisterSellerSideBar();