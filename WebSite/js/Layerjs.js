$(document).ready(function() {
    $(".ul_plan li").on("click", function(event) {
        event.preventDefault();
        $(".fulled").fadeIn();
    })
    $("#btnclose").on("click", function(event) {
        if ($(event.target).is("#btnclose") || $(event.target).is(".fix-box")) {
            event.preventDefault();
            $(".fulled").hide();
        }
    })
    $("#btnclose1").on("click", function (event) {
        if ($(event.target).is("#btnclose1") || $(event.target).is(".fix-box")) {
            event.preventDefault();
            $(".fulled").hide();
        }
    })
    $("#btnsave").on("click", function (event) {
        if ($(event.target).is("#btnsave") || $(event.target).is(".fix-box")) {
            event.preventDefault();
            $(".fulled").hide();
            SaveIn();
        }
    })
    $(document).keyup(function(event) {
        if (event.which == "27") {
            $(".fulled").fadeOut();
        }

    })
})