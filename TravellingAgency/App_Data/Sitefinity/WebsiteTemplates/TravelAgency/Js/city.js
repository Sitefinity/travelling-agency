$(document).ready(function() { 
    var location = window.location.href;
    var list = $("#city-attractions-list");
    var option = null;
    if (/hotels/i.test(location)) {
        option = "Hotels";
        alert(1);
    } else if (/restaurants/i.test(location)) {
        option = "Restaurants";
    }
    list.find("a").each(function () {
        if ($(this).text().trim() == option) {
            $(this).addClass("active");
            return;
        }
    });
});