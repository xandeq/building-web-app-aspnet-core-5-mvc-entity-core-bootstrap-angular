$(document).ready(function () {
    var button = $("#buyButton");
    button.on("click", function () {
        console.info("Buy Item");
    });

    var productInfo = $(".product-info li");
    productInfo.on("click", function () {
        console.log("You clicked on ", $(this).text());
    });

    var myForm = $("#myForm");
    myForm.hide();

    var loginToggle = $("#loginToggle");
    var popupForm = $(".popup-form");
    loginToggle.on("click", function () {
        popupForm.fadeToggle(1000);
    });
});