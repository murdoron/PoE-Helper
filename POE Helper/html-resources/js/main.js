$(function () {
    ini();
    
});

function ini() {
    hideError();

    checkForNewFilterVersion();
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

/**
 *
 */
function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}

/**
 *
 */
function checkForNewFilterVersion() {
    var result = JSON.parse(httpGet("https://api.github.com/repos/NeverSinkDev/NeverSink-Filter/releases/latest"));
    var dateRelease = new Date(result.published_at.toString())
    var dateFilter = new Date(cefCustomObject.getFilterChangedDate());

    if (dateRelease > dateFilter) {
        // New Filterversion
        $("#filter-new-version").show();
        $("#filter-info").attr("href", result.html_url);
    } else {
        // Filter uptodate
        $("#filter-new-version").hide();
    }
}