
$(document).ready(function () {
    $("#theForm").hide();

    $("#buy_button").on("click", function () {
        console.log("buying button clicked");
    });

    $(".product-info li").on("click", function () {
        console.log("You clicked on : " + $(this).text());
    });


    var login_toggle = $("#login_toggle");
    var pop_up_form = $(".pop_up_form");

    login_toggle.on("click", function () {
        pop_up_form.slideToggle(1000);
    })
});