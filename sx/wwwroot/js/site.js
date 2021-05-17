// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let fullprice = 0;
$(".fullprice").each(function () {
    fullprice += parseInt(this.innerText);
});
$(".count")[0].innerText = fullprice;

$(".nums").change(function () {
    let x = $("#" + this.attributes["id"].value + ".nums").val();
    let y = $("#" + this.attributes["id"].value + ".price")[0].innerHTML;
    let count = parseInt(x) * parseInt(y);
    let pr = 0;
    $("#" + this.attributes["id"].value + ".fullprice")[0].innerHTML = count;
    $(".fullprice").each(function () {
        pr += parseInt(this.innerText);
    });
    $(".count")[0].innerText= pr;
});
