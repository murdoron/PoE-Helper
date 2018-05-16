$(function () {
    ini();
    
});

function ini() {
    hideError();
}

/**
 * Displays the errorMsgBox
 */
function showError(error, addtionalInfo) {
    if ($("#errorMsgBox").is(":visible") === false) {
        $("#errorMsgBox").show();
    }

    setErrorBoxMsg(error, addtionalInfo);
}

/**
 * Set the text of the ErrorMsgBox
 */
function setErrorBoxMsg(error, addtionalInfo) {
    $("#errorMsgBoxError").text(error);
    $("#errorMsgBoxInfo").text(addtionalInfo);
}

/**
 * Hides the errorMsgBox
 */
function hideError() {
    if ($("#errorMsgBox").is(":visible")) {
        $("#errorMsgBox").hide();
    }

    setErrorBoxMsg("", "");
}