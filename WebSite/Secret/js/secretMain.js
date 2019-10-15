$(function(){
    startInit('mainFrame', 460);
});


var browserVersion = window.navigator.userAgent.toUpperCase();
var isOpera = false, isFireFox = false, isChrome = false, isSafari = false, isIE = false;
function reinitIframe(iframeId, minHeight) {
    try {
        var iframe = document.getElementById(iframeId);
        var bHeight = 0;
        if (isChrome == false && isSafari == false)
            bHeight = iframe.contentWindow.document.body.scrollHeight;

        var dHeight = 0;
        if (isFireFox == true)
            dHeight = iframe.contentWindow.document.documentElement.offsetHeight + 2;
        else if (isIE == false && isOpera == false)
            dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
        else if (isIE == true && ! -[1, ] == false) { } //ie9+
        else
            bHeight += 3;

        var height = Math.max(bHeight, dHeight);
        if (height < minHeight) height = minHeight;
        iframe.style.height = height + "px";
    } catch (ex) { }
}
function startInit(iframeId, minHeight) {
    isOpera = browserVersion.indexOf("OPERA") > -1 ? true : false;
    isFireFox = browserVersion.indexOf("FIREFOX") > -1 ? true : false;
    isChrome = browserVersion.indexOf("CHROME") > -1 ? true : false;
    isSafari = browserVersion.indexOf("SAFARI") > -1 ? true : false;
    if (!!window.ActiveXObject || "ActiveXObject" in window)
        isIE = true;
    window.setInterval("reinitIframe('" + iframeId + "'," + minHeight + ")", 100);
} 