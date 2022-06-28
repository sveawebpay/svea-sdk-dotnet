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


var shippingHandler = function (data) {
    var a = data;
    console.log('event: ' + data);

    if (data) {
        document.dispatchEvent(new CustomEvent("sveaCheckout:setIsLoading", { detail: { isLoading: true } }));
        console.log(data.type);
        console.log(JSON.stringify(data.detail));

        fetch('https://localhost:44345/api/svea/shippingTaxCalculation', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data.detail),
            })
            .then(response => response.json())
            .then(data => {
                console.log('Success:', data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });

        document.dispatchEvent(new CustomEvent("sveaCheckout:setIsLoading", { detail: { isLoading: false } }));
    }
}


$(function () {
    document.addEventListener("sveaCheckout:shippingConfirmed", shippingHandler);
});