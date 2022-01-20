var countDownTimerInterval;
$(document).ready(function () {
    countDownTimerInterval = setInterval(countDown, 1000);
    countDown();
});
var minute = parseInt($('div[count-down-timer-minute]').attr('count-down-timer-minute'));
var second = parseInt($('div[count-down-timer-second]').attr('count-down-timer-second'));

function countDown() {
    if (second == 0) {
        if (second == 0 && minute == 0) {
            $('#count-down-timer-box').parent().addClass('d-none');
            $('#send-user-activation-sms-box').removeClass('d-none');
            clearInterval(countDownTimerInterval);
        }
        else {
            minute--;
            second = 59;
        }
    }
    else {
        second--;
    }
    setCountDownTimeBox();
}

function setCountDownTimeBox() {
    $('#count-down-timer-box').html(`${minute}:${second < 10 ? '0' + second : second}`);
}

///////
function reSendActivationCode(phoneNumber, e, reSendSmsUrl) {
    showLoading();
    var objectToSend = {
        phoneNumber: phoneNumber,
        __RequestVerificationToken: getRVT(e)
    }
    $.post(reSendSmsUrl, objectToSend, function (data, status) {
        if (status == 'success' && data.isSuccessful) {
            showToastr('success', data.message);
            $('#activation-code-box').html(data.data.activationCode);
            $('#count-down-timer-box').parent().removeClass('d-none');
            $('#send-user-activation-sms-box').addClass('d-none');
            minute = 3;
            second = 0;
            setCountDownTimeBox();
            countDownTimerInterval = setInterval(countDown, 1000);
        }
        else {
            showToastr('error', data.message);
        }
    }).fail(function () {
        showErrorMessage();
    }).always(function () {
        hideLoading();
    });;
}
function getRVT(e) {
    return $(e).parents('form').find(`input[name="${rvt}"]`).val();
}

function loginWithPhoneNumberInIdentity(message) {
    showToastr('success', message);
    location.href = '/';
}