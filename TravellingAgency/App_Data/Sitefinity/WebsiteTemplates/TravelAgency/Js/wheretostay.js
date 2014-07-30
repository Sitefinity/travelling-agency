// A $( document ).ready() block.
$("#countries-list, #cities-list").hide();
$("#countriesBtn, #citiesBtn").removeClass('active');
$("#citiesBtn").addClass("vis-hidden");

$(document).ready(function() { 
  
  var activeCountry = $("#countries-list").find('.active').text().trim();
  if (activeCountry) {
    activeCountry += '<i class="fa fa-angle-down m-left-sm"></i>';
    $("#countriesBtn").html(activeCountry);
    $("#citiesBtn").removeClass("vis-hidden");
  }
  
  var activeCity = $("#cities-list").find('.active').text().trim();
  if (activeCity) {
      activeCity += '<i class="fa fa-angle-down m-left-sm"></i>';
      $("#citiesBtn").html(activeCity);
  }


  if ($("#countries-list").find(".active").length == 0)
      $("#all-countries").addClass("active");


  if ($("#cities-list").find(".active").length == 0) 
      $("#all-cities").addClass("active");
  
  $("#countriesBtn").click(function() {
      $("#countries-list").toggle();
      $("#citiesBtn").removeClass('active');
    if ($("#countries-list").is(":visible")) {
        $("#cities-list").hide();
        $("#countriesBtn").addClass('active');
    } else {
        $("#countriesBtn").removeClass('active');
    }
  });
   $("#citiesBtn").click(function() {
     $("#cities-list").toggle();
         $("#countriesBtn").removeClass('active');
     if ($("#cities-list").is(":visible")) {
         $("#countries-list").hide();
         $("#citiesBtn").addClass('active');
     } else {
         $("#citiesBtn").removeClass('active');
     }
   });

   $("#all-countries").click(function () {
       if (!$(this).hasClass("active")) {
           var location = window.location.href;
           if (location.lastIndexOf("/") + 1 == location.length) {
               location = location.substring(0, location.length - 1);
           }

           location = location.substring(0, location.lastIndexOf("/"));
           if ($("#cities-list").find(".active").length > 0 && !$("#all-cities").hasClass("active"))
               location = location.substring(0, location.lastIndexOf("/"));

           window.location.replace(location);
       }
   });

   $("#all-cities").click(function () {
       if (!$(this).hasClass("active")) {
           var location = window.location.href;
           if (location.lastIndexOf("/") + 1 == location.length) {
               location = location.substring(0, location.length - 1);
           }

           location = location.substring(0, location.lastIndexOf("/"));

           window.location.replace(location);
       }
   });
  
});



