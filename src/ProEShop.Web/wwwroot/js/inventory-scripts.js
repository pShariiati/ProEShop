$('#sidebar').on('hide.bs.collapse', function (e) {
    if (e.target == this) {
        $('#main-content').removeClass('col-lg-10');
    }
});
$('#sidebar').on('show.bs.collapse', function (e) {
    if (e.target == this) {
        $('#main-content').addClass('col-lg-10');
    }
});