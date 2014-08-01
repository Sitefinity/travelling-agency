
$(document).ready(function() { 
    var nav = $("#main-navigation");
    if (nav.find(".active").length == 0) {
        var location = window.location.href;
        var option = null;
        if (/country/i.test(location) || /city/i.test(location) || /hotels/i.test(location) || /hotel/i.test(location) || /restaurants/i.test(location) || /restaurant/i.test(location)) {
            option = "Countries";
        } else if (/festival/i.test(location)) {
            option = "Festivals";
        }
        nav.find("a").each(function () {
            if ($(this).text().trim() == option) {
                $(this).addClass("active");
                return;
            }
        });
    }
});
