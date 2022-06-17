var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "table-row-group") {
            content.style.display = "none";
        } else {
            content.style.display = "table-row-group";
        }
    });
}

var updateSettings = function(element) {
    var form = element.closest('form');
    form.find('[type="hidden"]:first').val(element.html());
    form.submit();
};


var shippingHandler = new function (data) {
    var a = data;
    console.log('event: ' + data);

    if (data) {
        document.dispatchEvent(new CustomEvent("sveaCheckout:setIsLoading", { detail: { isLoading: true } }));

    }
}



$(function () {

    document.addEventListener("sveaCheckout:shippingConfirmed", shippingHandler)

});