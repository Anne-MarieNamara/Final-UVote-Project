$(document).ready(function () {
    $(".answer").hide();
    $(".question").click(function () {
        $(this).next("p").slideToggle(500);
    });

});