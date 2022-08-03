var isAuthenticated;
var loginPageUrl;
$(document).ready(function () {
    var win = $(this);
    var mainInputEl = $('#main-search-input');
    if (win.width() > 456) {
        mainInputEl.attr('placeholder', 'نام کالا، برند یا دسته مورد نظر خود را جستجو نمایید ...');
    } else {
        mainInputEl.attr('placeholder', 'جستجو ...');
    }
    $(window).on('resize', function () {
        win = $(this);
        if (win.width() > 456) {
            mainInputEl.attr('placeholder', 'نام کالا، برند یا دسته مورد نظر خود را جستجو نمایید ...');
        } else {
            mainInputEl.attr('placeholder', 'جستجو ...');
        }
    });

    isAuthenticated = $('body').attr('is-authenticated') === 'true';
    loginPageUrl = $('body').attr('login-page-url');

    $('body').removeAttr('is-authenticated');
    $('body').removeAttr('login-page-url');
});

const firstLoginModalBody = `<div class="modal" id="first-login-modal">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-body text-center">
                <h5>
                    لطفا ابتدا وارد وبسایت شوید
                </h5>
                <a class="btn btn-secondary">ورود</a>
                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">بستن</button>
            </div>
        </div>
    </div>
</div>`;

function showFirstLoginModal() {
    if ($('#first-login-modal').length === 0) {
        $('body').append(firstLoginModalBody);
        $('#first-login-modal a').attr('href', loginPageUrl);
    }
    $('#first-login-modal').modal('show');
}