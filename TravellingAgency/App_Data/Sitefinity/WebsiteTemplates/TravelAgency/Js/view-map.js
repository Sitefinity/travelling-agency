$(document).ready(function () {
    $(".sfitemViewMapLnk").click(function () {
        $(this).toggleClass("dark");
        var btn = $(this);
        if ($("#map_wrapper_read").is(':visible')) {
            btn.text("View map");
        } else {
            btn.text("Close map");
        }
    });
});